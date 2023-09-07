using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

#region Constructors

    public TestController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#endregion

    [HttpGet("Delete")]
    public async Task<IActionResult> DeleteDb()
    {
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
        //10k rekordów wykonywalo sie 6,5 minuty
        await serviceWrapper.generator.GenerateUsers(1000);
        await serviceWrapper.generator.GenerateShifts(5, 10);
        await serviceWrapper.generator.GenerateLanes(10);
        await serviceWrapper.generator.GenerateReservations(500);
        return Ok();
    }
}