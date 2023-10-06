using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RaportController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public RaportController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }
    
    [HttpGet("MostWorkedHours")]
    public async Task<IActionResult> MostWorkedHours()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(1);
        var workerWithHours= await serviceWrapper.raport.MostWorkedHours(start,end );
        return Ok(workerWithHours);
    }

    [HttpGet("BestBuyingClient")]
    public async Task<IActionResult> BestBuyingClient()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(30);
        var clientWithInvoices = await serviceWrapper.raport.BestBuyingClient(start, end);
        return Ok(clientWithInvoices);
    }

    [HttpGet("BestSellingProducts")]
    public async Task<IActionResult> BestSellingProducts()
    {
        DateTime start = DateTime.Today;
        DateTime end = start.AddDays(30);
        var invoicesWithProducts = await serviceWrapper.raport.BestSellingProducts(start, end);
        return Ok(invoicesWithProducts);
    }
}