using MainBackend.DTO;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

//[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class SupplyController : ControllerBase
{
    private readonly IServiceWrapper service;

    public SupplyController(IServiceWrapper service)
    {
        this.service = service;
    }

    [HttpGet("GetOrder/{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var order = await service.supply.GetOrder(orderId);
        if (order != null)
            return Ok(order);
        return NotFound();
    }

    [HttpGet("GetFullfilledOrders")]
    public async Task<IActionResult> GetFullfilledOrders()
    {
        var orders = await service.supply.GetFullfilledOrders();
        if (orders != null)
            return Ok(orders);
        return NotFound();
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder(ICollection<Product> products)
    {
        if (await service.supply.CreateOrder(products))
            return Ok();
        return BadRequest();
    }
}