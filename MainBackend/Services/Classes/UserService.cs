using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class UserService : IUserService
{
    private IRepositoryWrapper repositoryWrapper;
    
    public UserService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }
    
    public async Task Create(int login)
    {
        User user = new User();
        user.Login = login;
        repositoryWrapper.normalDbWrapper.user.Create(user);
        await repositoryWrapper.normalDbWrapper.Save();
    }
}