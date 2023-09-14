using MainBackend.Databases.BowlingDb.RepositoryWrapper;
using MainBackend.Databases.BowlingDw.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public interface IRepositoryWrapper
{
    IRepositoryWrapperDb normalDbWrapper { get; }
    IRepositoryWrapperDw normalDwWrapper { get; }
}