using MainBackend.Databases.BowlingDb.RepositoryWrapper;

namespace MainBackend.Databases.Generic.Repositories;

public interface IRepositoryWrapper
{
#region Variables

    IRepositoryWrapperDb normalDbWrapper { get; }

#endregion
}