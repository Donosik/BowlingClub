using MainBackend.Databases.BowlingDb.RepositoryWrapper;
using MainBackend.Databases.BowlingDw.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
    public IRepositoryWrapperDb normalDbWrapper { get; }
    public IRepositoryWrapperDw normalDwWrapper { get; }

#region Constructors

    public RepositoryWrapper(IRepositoryWrapperDb normalDbWrapper, IRepositoryWrapperDw normalDwWrapper)
    {
        this.normalDbWrapper = normalDbWrapper;
        this.normalDwWrapper = normalDwWrapper;
    }

#endregion
}