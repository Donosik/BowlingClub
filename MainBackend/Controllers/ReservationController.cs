using System.Security.Claims;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Client")]
[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public ReservationController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("GetAllReservations")]
    public async Task<IActionResult> GetAllReservations()
    {
        var allReservations = await serviceWrapper.reservation.GetReservations();
        if (allReservations != null)
            return Ok(allReservations);
        return NotFound();
    }
    
    [Authorize(Policy = "Worker")]
    [HttpGet("GetAllReservations/{usersPerPage}/{currentPage}/{onlyNew}/{onlyWithoutInvoice}")]
    public async Task<IActionResult> GetAllReservations(int usersPerPage,int currentPage,bool onlyNew,bool onlyWithoutInvoice)
    {
        var allReservations = await serviceWrapper.reservation.GetReservations(usersPerPage,currentPage,onlyNew,onlyWithoutInvoice);
        if (allReservations != null)
            return Ok(allReservations);
        return NotFound();
    }

    [HttpGet("GetClientReservations")]
    public async Task<IActionResult> GetClientReservations()
    {
        var clientIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (clientIdClaim == null || !int.TryParse(clientIdClaim, out int clientId))
            return BadRequest("Client not found");
        var clientReservations = await serviceWrapper.reservation.GetReservationsByClient(clientId);
        if (clientReservations != null)
            return Ok(clientReservations);
        return NotFound();
    }
    
    [HttpGet("GetClientReservations/{usersPerPage}/{currentPage}/{onlyNew}/{onlyWithoutInvoice}")]
    public async Task<IActionResult> GetClientReservations(int usersPerPage,int currentPage,bool onlyNew,bool onlyWithoutInvoice)
    {
        var clientIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (clientIdClaim == null || !int.TryParse(clientIdClaim, out int clientId))
            return BadRequest("Client not found");
        var clientReservations = await serviceWrapper.reservation.GetReservationsByClient(clientId,usersPerPage,currentPage,onlyNew,onlyWithoutInvoice);
        if (clientReservations != null)
            return Ok(clientReservations);
        return NotFound();
    }

    [HttpPost("MakeReservation")]
    public async Task<IActionResult> MakeReservation(ReservationForm reservationForm)
    {
        var clientIdClaim = User.FindFirst(ClaimTypes.Name).Value;
        if (clientIdClaim == null || !int.TryParse(clientIdClaim, out int clientId))
            return BadRequest("Client not found");
        if (await serviceWrapper.reservation.MakeReservation(reservationForm, clientId))
            return Ok();
        return BadRequest();
    }
}