using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Classes;

namespace MainBackend.Services.Interfaces;

public interface IUserService
{
#region Get

    Task<ICollection<User>> GetUsers();

#endregion

#region Create

    Task<bool> RegisterClient(RegisterForm registerForm);
    Task<bool> RegisterWorker(RegisterForm registerForm);
    Task<User> Login(LoginForm loginForm);
    Task<string> GenerateToken(User user);

#endregion

#region Delete

    Task<bool> DeleteUser(int id);

#endregion
}