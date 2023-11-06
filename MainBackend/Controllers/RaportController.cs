using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class RaportController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public RaportController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [Authorize(Policy = "Worker")]
    [HttpGet("MostWorkedHours")]
    public async Task<IActionResult> MostWorkedHours()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(1);
        var workerWithHours = await serviceWrapper.raport.MostWorkedHours(start, end);
        return Ok(workerWithHours);
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("BestBuyingClient")]
    public async Task<IActionResult> BestBuyingClient()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(30);
        var clientWithInvoices = await serviceWrapper.raport.BestBuyingClient(start, end);
        return Ok(clientWithInvoices);
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("BestSellingProducts")]
    public async Task<IActionResult> BestSellingProducts()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(30);
        var invoicesWithProducts = await serviceWrapper.raport.BestSellingProducts(start, end);
        return Ok(invoicesWithProducts);
    }

#endregion
}