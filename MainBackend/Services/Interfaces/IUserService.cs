using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Classes;

namespace MainBackend.Services.Interfaces;

public interface IUserService
{
#region Get
    
    Task<ICollection<User>> GetUsers();
    Task<ICollection<Worker>> GetWorkers();

#endregion

#region Create

    Task<bool> RegisterClient(RegisterForm registerForm);
    Task<bool> RegisterClientGoogle();
    Task<bool> RegisterWorker(RegisterForm registerForm);
    Task<User> Login(LoginForm loginForm);
    Task<User> LoginGoogle();
    Task<string> GenerateToken(User user);

#endregion

#region Put

    Task<bool> ChangeToAdmin(int workerId, bool isAdmin);

#endregion

#region Delete

    Task<bool> DeleteUser(int id);

#endregion
}