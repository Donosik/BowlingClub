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
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)

    {
        return Ok();
    }
}