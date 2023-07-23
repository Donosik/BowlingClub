using MainBackend.Databases.BowlingDb.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public class RepositoryWrapper : IRepositoryWrapper
{
#region Variables

    public IRepositoryWrapperDb normalDbWrapper { get; }

#endregion

#region Constructors

    public RepositoryWrapper(IRepositoryWrapperDb normalDbWrapper)
    {
        this.normalDbWrapper = normalDbWrapper;
    }

#endregion
}