using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public class RepositoryWrapperDb : IRepositoryWrapperDb
{

#region Variables

    public IUserRepository user { get; }

#endregion
    
#region Private Variables

    private readonly Context.BowlingDb dbContext;

#endregion

#region Constructors

    public RepositoryWrapperDb(Context.BowlingDb dbContext, IUserRepository user)
    {
        this.dbContext = dbContext;
        this.user = user;
    }

#endregion

#region Methods

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

#endregion

#region Private Methods

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            dbContext.Dispose();
    }

#endregion
}