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
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder(Order order)
    {
        if (await service.order.CreateOrder(order))
            return Ok();
        else
            return BadRequest();
    }
}