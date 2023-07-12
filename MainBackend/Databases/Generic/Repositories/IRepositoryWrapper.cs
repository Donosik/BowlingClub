using MainBackend.Databases.BowlingDb.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public interface IRepositoryWrapper
{
    IRepositoryWrapperDb normalDbWrapper { get; }
}