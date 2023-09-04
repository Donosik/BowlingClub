using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
    IUserRepository user { get; }
    IPersonRepository person { get; }
    
    IClientRepository client { get; }

#region Methods

    Task<bool> Save();
    void Dispose();

#endregion
}