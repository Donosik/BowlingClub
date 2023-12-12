using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public class UserRepository : GenericRepository<User>,IUserRepository
{
    public UserRepository(Context.SuppliesDb dbContext) : base(dbContext)
    {
    }
}