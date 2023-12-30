using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class BarInventoryRepository : GenericRepository<Entities.Inventory>,IBarInventoryRepository
{
    public BarInventoryRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }

    public async Task<Inventory> GetByProductName(string name)
    {
        return await GetQuery().Where(t => t.Name == name).Where(t=>t.Invoice==null).FirstOrDefaultAsync();
    }
    
    public async Task<ICollection<Inventory>> GetByProductName(string name,int howMany)
    {
        return await GetQuery().Where(t => t.Name == name).Where(t=>t.Invoice==null).Take(howMany).ToListAsync();
    }

    public async Task<ICollection<Inventory>> GetALlWithInvoices()
    {
        return await GetQuery().Include(t=>t.Invoice).ToListAsync();
    }
}