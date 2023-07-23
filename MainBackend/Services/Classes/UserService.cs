using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class UserService : IUserService
{
#region Private Variables

    private IRepositoryWrapper repositoryWrapper;

#endregion

#region Constructors

    public UserService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

#endregion

#region Methods

#region Create

    public async Task Create(int login)
    {
        User user = new User();
        repositoryWrapper.normalDbWrapper.user.Create(user);
        await repositoryWrapper.normalDbWrapper.Save();
    }

#endregion

#endregion
}