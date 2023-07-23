using MainBackend.DTO;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;


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
    public async Task<IActionResult> Register(RegisterForm registerForm)
    {
        if (await serviceWrapper.userService.Register(registerForm))
            return Ok();
        return BadRequest("Not implemented");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        if (await serviceWrapper.userService.Login(loginForm))
        {
            //TODO: Generate Token
            return Ok();
        }
        return BadRequest("Not implemented");
    }

#endregion

#endregion
}