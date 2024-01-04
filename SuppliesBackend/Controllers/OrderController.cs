using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.DTO;
using SuppliesBackend.Services.Wrapper;

namespace SuppliesBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IServiceWrapper service;

    public OrderController(IServiceWrapper service)
    {
        this.service = service;
    }

    [HttpGet("GetOrder/{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var order = await service.order.GetOrder(orderId);
        if (order != null)
            return Ok(order);
        else
            return NotFound();
    }

    [HttpGet("GetUnfullfilledOrders")]
    public async Task<IActionResult> GetUnfullfilledOrders()
    {
        var orders = await service.order.GetUnfullfilledOrders();
        if (orders != null)
            return Ok(orders);
        else
            return NotFound();
    }

    [HttpGet("GetFullfilledOrders")]
    public async Task<IActionResult> GetFullfilledOrders()
    {
        var orders = await service.order.GetFullfilledOrders();
        if (orders != null)
        {
            if (await service.order.TakeOrders(orders))
                return Ok(orders);
            return BadRequest();
        }
        else
            return NotFound();
    }


    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(ICollection<ProductDTO> products)
    {
        if (await service.order.CreateOrder(products))
            return Ok();
        else
            return BadRequest();
    }

    [HttpPost("FullfillOrder/{orderId}")]
    public async Task<IActionResult> FullfillOrder(int orderId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int clientId))
            return BadRequest("Client not found");
        
        if (await service.order.FullfillOrder(orderId,clientId))
            return Ok();
        else
            return BadRequest();
    }
    
    [HttpGet("MyOrders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int clientId))
            return BadRequest("Client not found");
        var orders = await service.order.GetMyOrders(clientId);
        if (orders != null)
            return Ok(orders);
        return NotFound();
    }
}