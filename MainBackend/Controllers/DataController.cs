using MainBackend.DTO;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public DataController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetOpenHours()
    {
        try
        {
            ICollection<OpenHour> openHours = await serviceWrapper.data.GetOpenHours();
            return Ok(openHours);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

#endregion

#region Post

    [HttpPost]
    public async Task<IActionResult> CreateDefaultOpenHours()
    {
        if (await serviceWrapper.data.CreateDefaultOpenHours())
            return Ok();
        return BadRequest("Something went wrong");
    }

#endregion

#region Put

    [HttpPut]
    public async Task<IActionResult> ChangeOpenHours(ICollection<OpenHour> openHours)
    {
        if (await serviceWrapper.data.ChangeOpenHours(openHours))
            return Ok("Everything is ok");
        return BadRequest("Something went wrong");
    }

#endregion
}