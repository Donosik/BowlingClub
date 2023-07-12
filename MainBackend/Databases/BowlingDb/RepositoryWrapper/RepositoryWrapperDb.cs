using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public class RepositoryWrapperDb : IRepositoryWrapperDb
{
    private readonly Context.BowlingDb dbContext;

    public IUserRepository user { get; }

    public RepositoryWrapperDb(Context.BowlingDb dbContext, IUserRepository user)
    {
        this.dbContext = dbContext;
        this.user = user;
    }

    public bool Save()
    {
        int result = dbContext.SaveChanges();
        if (result > 0)
            return true;
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            dbContext.Dispose();
    }
}