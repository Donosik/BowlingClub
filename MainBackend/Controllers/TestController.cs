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
            bool result=await serviceWrapper.person.DeletePerson(person.Id);
            if (result == false)
            {
                return BadRequest("Something went wrong");
            }
        }
        return Ok();
    }

    [HttpGet("Generate")]
    public async Task<IActionResult> GenerateDb()
    {
        //10k rekordów wykonywalo sie 6,5 minuty
        await serviceWrapper.generator.GenerateUsers(10000);
        await serviceWrapper.generator.GenerateShifts(5,10);
        return Ok();
    }
}