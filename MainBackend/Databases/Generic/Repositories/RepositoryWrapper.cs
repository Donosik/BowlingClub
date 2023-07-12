using MainBackend.Databases.BowlingDb.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    public IRepositoryWrapperDb normalDbWrapper { get; }

    public RepositoryWrapper(IRepositoryWrapperDb normalDbWrapper)
    {
        this.normalDbWrapper = normalDbWrapper;
    }
}