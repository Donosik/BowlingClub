using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.DTO;

namespace SuppliesBackend.Services.Interfaces;

public interface IUserService
{
    Task<bool> Register(LoginForm loginForm);
    Task<User> Login(LoginForm loginForm);

    Task<string> GenerateToken(User user);
}