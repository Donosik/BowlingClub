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
        var persons = await serviceWrapper.personService.GetPersons();
        foreach (var person in persons)
        {
            bool result=await serviceWrapper.personService.DeletePerson(person.Id);
            if (result == false)
            {
                return BadRequest("Something went wrong");
            }
        }
        return Ok();
    }
}