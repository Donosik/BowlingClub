using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

//[Authorize(Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public TestController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteDb()
    {
        var invoices = await serviceWrapper.invoice.GetInvoices();
        foreach (var invoice in invoices)
        {
            bool result = await serviceWrapper.invoice.DeleteInvoice(invoice.Id);
            if (result == false)
            {
                return BadRequest("Something went wrong");
            }
        }
        var persons = await serviceWrapper.person.GetPersons();
        foreach (var person in persons)
        {
            bool result = await serviceWrapper.person.DeletePerson(person.Id);
            if (result == false)
            {
                return BadRequest("Something went wrong");
            }
        }

        var workSchedules = await serviceWrapper.workSchedule.GetWorkSchedules();
        foreach (var shift in workSchedules)
        {
            bool result = await serviceWrapper.workSchedule.DeleteShift(shift.Id);
            if (result == false)
                return BadRequest("Something went wrong");
        }

        var lanes = await serviceWrapper.lane.GetLanes();
        foreach (var lane in lanes)
        {
            bool result = await serviceWrapper.lane.DeleteLane(lane.Id);
            if (result == false)
                return BadRequest("Something went wrong");
        }

        var reservations = await serviceWrapper.reservation.GetReservations();
        foreach (var reservation in reservations)
        {
            bool result = await serviceWrapper.reservation.DeleteReservation(reservation.Id);
            if (result == false)
                return BadRequest("Something went wrong");
        }

        return Ok();
    }

    [HttpGet("Generate")]
    public async Task<IActionResult> GenerateDb()
    {
        await serviceWrapper.generator.GenerateUsers(1000);
        await serviceWrapper.generator.GenerateShifts(5, 10);
        await serviceWrapper.generator.GenerateLanes(10);
        await serviceWrapper.generator.GenerateReservations(30, 50);
        await serviceWrapper.generator.GenerateInventoryItems(1000);
        await serviceWrapper.generator.GenerateInvoices(500);
        //Rzeczy na stałe
        await serviceWrapper.generator.GenerateAdmin();
        await serviceWrapper.data.CreateDefaultOpenHours();
        await serviceWrapper.promotion.CreateDefaultPromotions();
        return Ok();
    }

    [HttpGet("GenerateAdmin")]
    public async Task<IActionResult> GenerateAdmin()
    {
        await serviceWrapper.generator.GenerateAdmin();
        return Ok();
    }

    [HttpGet("TestCreateOrder")]
    public async Task<IActionResult> TestCreateOrder()
    {
        var result=await serviceWrapper.supply.CreateNecessaryOrders();
        return Ok(result);
    }

#endregion
}