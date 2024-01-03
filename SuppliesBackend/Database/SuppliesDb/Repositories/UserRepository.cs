using Microsoft.EntityFrameworkCore;
using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(Context.SuppliesDb dbContext) : base(dbContext)
    {
    }

    public async Task<User> GetUserWithOrders(int userId)
    {
        return await GetQuery().Include(x => x.FullfilledOrders).FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User> GetOrdersWithProducts(int clientId)
    {
        User user = await dbContext.Set<User>().Include(x => x.FullfilledOrders).ThenInclude(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == clientId);
        return user;
    }
}