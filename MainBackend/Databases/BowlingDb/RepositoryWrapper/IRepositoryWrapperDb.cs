using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
#region Variables

    IUserRepository user { get; }

#endregion

#region Methods

    Task<bool> Save();
    void Dispose();

#endregion
}