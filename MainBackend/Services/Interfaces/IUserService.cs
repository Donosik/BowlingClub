using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;
using MainBackend.Services.Classes;

namespace MainBackend.Services.Interfaces;

public interface IUserService
{
#region Get
    
    Task<ICollection<User>> GetUsers();
    Task<ICollection<User>> GetUsers(int usersPerPage, int currentPage);
    Task<ICollection<Worker>> GetWorkers();
    Task<ICollection<User>> GetClients();

#endregion

#region Create

    Task<bool> RegisterClient(RegisterForm registerForm);
    Task<bool> RegisterClientGoogle(RegisterFormGoogle registerForm);
    Task<bool> RegisterWorker(RegisterForm registerForm);
    Task<User> Login(LoginForm loginForm);
    Task<User> LoginGoogle(string email);
    Task<string> GenerateToken(User user);

#endregion

#region Put

    Task<bool> ChangeToAdmin(int workerId, bool isAdmin);

    Task<bool> ChangePassword(int userId, string newPassword);
    Task<bool> Deactivate(int workerId, bool deactivate);

#endregion

#region Delete

    Task<bool> DeleteUser(int id);

#endregion
}