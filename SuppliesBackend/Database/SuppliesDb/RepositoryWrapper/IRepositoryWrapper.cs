using SuppliesBackend.Database.SuppliesDb.Repositories;
using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;

public interface IRepositoryWrapper : IDisposable
{
    IProductRepository product { get; }
    IOrderRepository order { get; }
    IUserRepository user { get; }
    
    Task<bool> Save(int entities = 1);
    void Dispose();
}