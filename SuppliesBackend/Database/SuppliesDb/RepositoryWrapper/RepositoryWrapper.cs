using SuppliesBackend.Database.SuppliesDb.Repositories;

namespace SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;

public class RepositoryWrapper : IRepositoryWrapper
{
    public IProductRepository product { get; }
    public IOrderRepository order { get; }
    public IUserRepository user { get; }
    private readonly Context.SuppliesDb dbContext;
    public RepositoryWrapper(IProductRepository product, Context.SuppliesDb dbContext, IOrderRepository order, IUserRepository user)
    {
        this.product = product;
        this.dbContext = dbContext;
        this.order = order;
        this.user = user;
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
        if (disposing)
            dbContext.Dispose();
    }
}