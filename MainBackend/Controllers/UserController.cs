using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
#region Private Variables

    private readonly IServiceWrapper serviceWrapper;

#endregion

#region Constructors

    public UserController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#endregion

#region Methods

#region Post

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register()
    {
        return BadRequest("Not implemented");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login()
    {
        return BadRequest("Not implemented");
    }

#endregion

#endregion
}