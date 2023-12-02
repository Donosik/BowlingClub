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
    [HttpGet("MostWorkedHours/{howManyDaysAgo}/{howManyTop}")]
    public async Task<IActionResult> MostWorkedHours(int howManyDaysAgo, int howManyTop)
    {
        var workerWithHours = await serviceWrapper.raport.MostWorkedHours(howManyDaysAgo, howManyTop);
        return Ok(workerWithHours);
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("BestBuyingClient/{howManyDaysAgo}/{howManyTop}")]
    public async Task<IActionResult> BestBuyingClient(int howManyDaysAgo, int howManyTop)
    {
        var clientWithInvoices = await serviceWrapper.raport.BestBuyingClient(howManyDaysAgo, howManyTop);
        return Ok(clientWithInvoices);
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("BestSellingProducts/{howManyDaysAgo}/{howManyTop}")]
    public async Task<IActionResult> BestSellingProducts(int howManyDaysAgo, int howManyTop)
    {
        var invoicesWithProducts = await serviceWrapper.raport.BestSellingProducts(howManyDaysAgo, howManyTop);
        return Ok(invoicesWithProducts);
    }

#endregion
}