using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class RegulationController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public RegulationController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetRegulations()
    {
        ICollection<Regulation> promotions = await serviceWrapper.regulation.GetRegulations();
        return Ok(promotions);
    }

#endregion

#region Post

    [HttpPost]
    public async Task<IActionResult> AddRegulation(Regulation regulation)
    {
        if (await serviceWrapper.regulation.AddRegulation(regulation))
            return Ok();
        return BadRequest();
    }

#endregion

#region Put

    [HttpPut]
    public async Task<IActionResult> ChangePromotions(ICollection<Regulation> regulations)
    {
        if (await serviceWrapper.regulation.ChangeRegulations(regulations))
            return Ok();
        return BadRequest();
    }

#endregion

#region Delete

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReulation(int id)
    {
        if (await serviceWrapper.regulation.DeleteRegulation(id))
            return Ok();
        return BadRequest();
    }

#endregion
}