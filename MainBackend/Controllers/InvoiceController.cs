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
    [HttpGet("WorkerIvoices/{workerId}")]
    public async Task<IActionResult> WorkerInvoices(int workerId)
    {
        return BadRequest("Not Implemented");
    }

    [Authorize(Policy = "Client")]
    [HttpGet("ClientInvoices/{clientId}")]
    public async Task<IActionResult> ClientInvoices(int clientId)
    {
        return BadRequest("Not implemented");
    }

#endregion

#region Post

    [Authorize(Policy = "Worker")]
    [HttpPost("CreateInvoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceForm invoiceForm)
    {
        return BadRequest("Not Implemented");
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