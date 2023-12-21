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
            return Ok(orders);
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

    [HttpPost("FullfillOrder/{orderId}")]
    public async Task<IActionResult> FullfillOrder(int orderId)
    {
        if (await service.order.FullfillOrder(orderId))
            return Ok();
        else
            return BadRequest();
    }
}