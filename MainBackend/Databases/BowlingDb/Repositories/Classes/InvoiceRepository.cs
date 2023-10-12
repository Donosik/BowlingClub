using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Invoice>> GetWorkerInvoices(int workerId)
    {
        return await dbContext.Set<Invoice>().Where(i => i.WorkerId == workerId).ToListAsync();
    }

    public async Task<ICollection<Invoice>> GetClientInvoices(int clientId)
    {
        return await dbContext.Set<Invoice>().Where(i => i.ClientId == clientId).ToListAsync();
    }
}