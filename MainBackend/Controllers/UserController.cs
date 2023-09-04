﻿using MainBackend.Databases.BowlingDb.Entities;
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

#region Post

    [AllowAnonymous]
    [HttpPost("RegisterClient")]
    public async Task<IActionResult> RegisterClient(RegisterForm registerForm)
    {
        if (await serviceWrapper.user.RegisterClient(registerForm))
            return Ok();
        return NotFound("User with that login already exists or client account for that email already exists");
    }

    //TODO: Tymczasowo jest anonymous
    [AllowAnonymous]
    [HttpPost("RegisterWorker")]
    public async Task<IActionResult> RegisterWorker(RegisterForm registerForm)
    {
        if (await serviceWrapper.user.RegisterWorker(registerForm))
            return Ok();
        return NotFound("User with that login already exists or worker account for that email already exists");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        User user = await serviceWrapper.user.Login(loginForm);
        if (user != null)
        {
            return Ok(await serviceWrapper.user.GenerateToken(user));
        }

        return NotFound("User with that login and password doesn't exist");
    }

#endregion
}