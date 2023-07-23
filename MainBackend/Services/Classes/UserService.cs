using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class UserService : IUserService
{
    private IRepositoryWrapper repositoryWrapper;

#region Constructors

    public UserService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

#endregion

#region Create

    public async Task<bool> Register(RegisterForm registerForm)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Login(LoginForm loginForm)
    {
        throw new NotImplementedException();
    }

#endregion
}