using Microsoft.EntityFrameworkCore;
using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(Context.SuppliesDb dbContext) : base(dbContext)
    {
    }
    
    public async Task<Product> GetByName(string name)
    {
        return await dbContext.Set<Product>().FirstOrDefaultAsync(x=>x.Name == name);
    }
}