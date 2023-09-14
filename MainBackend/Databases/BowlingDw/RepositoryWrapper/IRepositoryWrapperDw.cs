namespace MainBackend.Databases.BowlingDw.RepositoryWrapper;

public interface IRepositoryWrapperDw : IDisposable
{
    
    
    Task<bool> Save(int entities = 1);
    void Dispose();
}