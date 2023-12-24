using Microsoft.EntityFrameworkCore;
using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public class OrderRepository : GenericRepository<Order> , IOrderRepository
{
    public OrderRepository(Context.SuppliesDb dbContext) : base(dbContext)
    {
    }
    
    public async Task<ICollection<Order>> GetAll()
    {
        return await GetQuery().Include(o=>o.Products).ToListAsync();
    }
}