using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Classes;

namespace MainBackend.Services.Interfaces;

public interface IUserService
{
#region Create

    Task<bool> Register(RegisterForm registerForm);
    Task<User> Login(LoginForm loginForm);
    Task<string> GenerateToken(User user);

#endregion
}