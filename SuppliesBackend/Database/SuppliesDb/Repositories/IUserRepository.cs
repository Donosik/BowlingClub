using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUserWithOrders(int userId);
    Task<User> GetOrdersWithProducts(int clientId);
}