using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
    #region Entity Repositories

    IUserRepository user { get; }

    #endregion
    
    Task<bool> Save();
}