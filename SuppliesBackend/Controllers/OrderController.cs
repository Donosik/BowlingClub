using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuppliesBackend.Database.SuppliesDb.Entities;
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
    
    [HttpGet("OrderStatus/{orderId}")]
    public async Task<IActionResult> GetOrderStatus(int orderId)
    {
        if(await service.order.GetOrderStatus(orderId))
            return Ok();
        else
            return NotFound();
    }
    
    [HttpGet("GetCompletedOrder/{orderId}")]
    public async Task<IActionResult> GetCompletedOrder(int orderId)
    {
        var order = await service.order.GetCompletedOrder(orderId);
        if (order != null)
            return Ok(order);
        else
            return NotFound();
    }
    
    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(ICollection<Product> products)
    {
        if (await service.order.CreateOrder(products))
            return Ok();
        else
            return BadRequest();
    }
    
    [HttpPost("FulfillOrder/{orderId}")]
    public async Task<IActionResult> FulfillOrder(int orderId)
    {
        if (await service.order.FulfillOrder(orderId))
            return Ok();
        else
            return BadRequest();
    }
}