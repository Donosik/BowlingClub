using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IUserService
{
#region Create

    Task<bool> Register(RegisterForm registerForm);
    Task<bool> Login(LoginForm loginForm);

#endregion
}