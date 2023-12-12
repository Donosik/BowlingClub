using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<Product> GetByName(string name);
}