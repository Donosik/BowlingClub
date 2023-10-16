using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Exceptions;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[Authorize(Policy = "Client")]
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

#region Get

    [Authorize(Policy = "Worker")]
    [HttpGet("AllWorkers")]
    public async Task<IActionResult> AllWorkers()
    {
        return Ok(await serviceWrapper.user.GetWorkers());
    }

#endregion

#region Post

    [AllowAnonymous]
    [HttpPost("RegisterClient")]
    public async Task<IActionResult> RegisterClient(RegisterForm registerForm)
    {
        try
        {
            if (await serviceWrapper.user.RegisterClient(registerForm))
                return Ok();
        }
        //TODO: catch do niepoprawnie sparsowanych danych, np. za krotkie hasło
        catch (LoginAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (PersonAlreadyHasAccountException ex)
        {
            return Conflict(ex.Message);
        }
        catch (RegisterFormException ex)
        {
            return 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return BadRequest("Something went wrong");
    }

    [Authorize(Policy = "Admin")]
    [HttpPost("RegisterWorker")]
    public async Task<IActionResult> RegisterWorker(RegisterForm registerForm)
    {
        try
        {
            if (await serviceWrapper.user.RegisterWorker(registerForm))
                return Ok();
        }
        //TODO: catch do niepoprawnie sparsowanych danych, np. za krotkie hasło
        catch (LoginAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (PersonAlreadyHasAccountException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return BadRequest("Something went wrong");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginForm loginForm)
    {
        User user = await serviceWrapper.user.Login(loginForm);
        if (user != null)
            return Ok(await serviceWrapper.user.GenerateToken(user));
        return NotFound("User with that login and password doesn't exist");
    }

#endregion

#region Put

    [Authorize(Policy = "Admin")]
    [HttpPut("ChangeToAdmin/{workerId}/{isAdmin}")]
    public async Task<IActionResult> ChangeToAdmin(int workerId, bool isAdmin)
    {
        if (await serviceWrapper.user.ChangeToAdmin(workerId, isAdmin))
            return Ok();
        return NotFound();
    }

    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword(string newPassword)
    {
        return Ok("Not implemented");
    }

#endregion
}