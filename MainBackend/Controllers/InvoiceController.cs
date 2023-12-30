using System.Security.Claims;
using MainBackend.DTO;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Client")]
[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public InvoiceController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [Authorize(Policy = "Worker")]
    [HttpGet("AllInvoices")]
    public async Task<IActionResult> AllInvoices()
    {
        return BadRequest("Not Implemented");
    }

    [Authorize(Policy = "Client")]
    [HttpGet("ClientInvoices")]
    public async Task<IActionResult> ClientInvoices()
    {
        //Get client id from token
        return BadRequest("Not implemented");
    }

#endregion

#region Post

    [Authorize(Policy = "Worker")]
    [HttpPost("CreateInvoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceForm invoiceForm)
    {
        var workerIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (workerIdClaim == null || !int.TryParse(workerIdClaim, out int workerId))
            return BadRequest("Worker not found");

        if (await serviceWrapper.invoice.AddInvoice(invoiceForm, workerId))
            return Ok();
        return BadRequest();
    }

#endregion

#region Delete

    [Authorize(Policy = "Worker")]
    [HttpDelete("DeleteInvoice/{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        return BadRequest("Not Implemented");
    }

#endregion
}