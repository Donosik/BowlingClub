using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public class RepositoryWrapperDb : IRepositoryWrapperDb
{
    public IUserRepository user { get; }
    private readonly Context.BowlingDb dbContext;

#region Constructors

    public RepositoryWrapperDb(Context.BowlingDb dbContext, IUserRepository user)
    {
        this.dbContext = dbContext;
        this.user = user;
    }

#endregion

    public async Task<bool> Save()
    {
        int result = await dbContext.SaveChangesAsync();
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