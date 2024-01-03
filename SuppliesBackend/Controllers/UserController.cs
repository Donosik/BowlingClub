using Microsoft.AspNetCore.Mvc;
using SuppliesBackend.DTO;
using SuppliesBackend.Services.Wrapper;

namespace SuppliesBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IServiceWrapper service;

    public UserController(IServiceWrapper service)
    {
        this.service = service;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(LoginForm loginForm)
    {
        if (await service.user.Register(loginForm))
            return Ok();
        return BadRequest();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        var user = await service.user.Login(loginForm);
        if (user != null)
            return Ok(await service.user.GenerateToken(user));
        return NotFound();
    }
}