using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IBarInventoryRepository : IGenericRepository<Inventory>
{
    Task<Inventory> GetByProductName(string name);
    Task<ICollection<Inventory>> GetByProductName(string name,int howMany);
    Task<ICollection<Inventory>> GetALlWithInvoices();
}