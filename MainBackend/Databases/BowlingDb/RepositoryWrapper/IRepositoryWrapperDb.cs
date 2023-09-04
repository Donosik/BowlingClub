using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
    IUserRepository user { get; }
    IPersonRepository person { get; }
    IClientRepository client { get; }
    IWorkerRepository worker { get; }

#region Methods

    Task<bool> Save(int entities = 1);
    void Dispose();

#endregion
}