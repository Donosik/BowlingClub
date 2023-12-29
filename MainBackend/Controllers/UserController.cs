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

    public UserController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }

#region Get

    [Authorize(Policy = "Worker")]
    [HttpGet("AllWorkers")]
    public async Task<IActionResult> AllWorkers()
    {
        return Ok(await serviceWrapper.user.GetWorkers());
    }

    [Authorize(Policy = "Worker")]
    [HttpGet("AllUsers/{usersPerPage}/{currentPage}")]
    public async Task<IActionResult> AllUsers(int usersPerPage, int currentPage)
    {
        return Ok(await serviceWrapper.user.GetUsers(usersPerPage, currentPage));
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
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return BadRequest("Something went wrong");
    }

    [AllowAnonymous]
    [HttpPost("RegisterClientGoogle")]
    public async Task<IActionResult> RegisterClientGoogle(RegisterFormGoogle registerForm)
    {
        try
        {
            if (await serviceWrapper.user.RegisterClientGoogle(registerForm))
                return Ok();
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
            return BadRequest(ex.Message);
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

    [AllowAnonymous]
    [HttpPost("LoginGoogle")]
    public async Task<IActionResult> LoginGoogle([FromBody]string email)
    {
        User user = await serviceWrapper.user.LoginGoogle(email);
        if (user != null)
            return Ok(await serviceWrapper.user.GenerateToken(user));
        return NotFound("User with that google account doesn't exist");
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
    
    [HttpPut("ChangePassword/{userId}/{newPassword}")]
    public async Task<IActionResult> ChangePassword(int userId, string newPassword)
    {
        if (await serviceWrapper.user.ChangePassword(userId, newPassword))
            return Ok();
        return NotFound();
    }

    [Authorize(Policy = "Admin")]
    [HttpPut("Deactivate/{workerId}/{deactivate}")]
    public async Task<IActionResult> Deactivate(int workerId, bool deactivate)
    {
        if (await serviceWrapper.user.Deactivate(workerId, deactivate))
            return Ok();
        return NotFound();
    }

#endregion

#region Delete

    [Authorize(Policy = "Admin")]
    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (await serviceWrapper.user.DeleteUser(id))
            return Ok();
        return NotFound();
    }

#endregion
}