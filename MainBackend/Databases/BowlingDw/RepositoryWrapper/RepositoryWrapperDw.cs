namespace MainBackend.Databases.BowlingDw.RepositoryWrapper;

public class RepositoryWrapperDw : IRepositoryWrapperDw
{
    private readonly Context.BowlingDw dbContext;

    public RepositoryWrapperDw(Context.BowlingDw dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<bool> Save(int entities = 1)
    {
        int result = await dbContext.SaveChangesAsync();
        if (result >= entities)
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
        if(disposing)
            dbContext.Dispose();
    }
}