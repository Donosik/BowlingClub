using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Worker")]
[ApiController]
[Route("[controller]")]
public class PromotionController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public PromotionController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetPromotions()
    {
        try
        {
            ICollection<Promotion> promotions = await serviceWrapper.promotion.GetPromotions();
            return Ok(promotions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

#endregion

#region Post

    [HttpPost]
    public async Task<IActionResult> CreateDefaultPromotions()
    {
        if (await serviceWrapper.promotion.CreateDefaultPromotions())
            return Ok();
        return BadRequest();
    }

#endregion

#region Put

    [HttpPut]
    public async Task<IActionResult> ChangePromotions(ICollection<Promotion> promotions)
    {
        if (await serviceWrapper.promotion.ChangePromotions(promotions))
            return Ok();
        return BadRequest();
    }

#endregion
}