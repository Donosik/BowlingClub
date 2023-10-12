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

    [Authorize(Policy = "Worker")]
    [HttpGet("WorkerIvoices/{workerId}")]
    public async Task<IActionResult> WorkerInvoices(int workerId)
    {
        return Ok("Not Implemented");
    }

    [HttpGet("ClientInvoices/{clientId}")]
    public async Task<IActionResult> ClientInvoices(int clientId)
    {
        return Ok("Not implemented");
    }

    [Authorize(Policy = "Worker")]
    [HttpPost("CreateInvoice")]
    public async Task<IActionResult> CreateInvoice(InvoiceForm invoiceForm)
    {
        return Ok("Not Implemented");
    }

    [Authorize(Policy = "Worker")]
    [HttpDelete("DeleteInvoice/{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        return Ok("Not Implemented");
    }
}