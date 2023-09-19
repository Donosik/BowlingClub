using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.Databases.BowlingDw.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDw.Repositories.Classes;

public class FactInvoiceRepository : GenericRepository<FactInvoice>,IFactInvoiceRepository
{
    public FactInvoiceRepository(Context.BowlingDw dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<FactInvoice>> GetAllWithDims()
    {
        return await dbContext.Set<FactInvoice>()
            .Include(f => f.Client)
            .Include(f => f.IssueDate)
            .Include(f => f.DueDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<FactInvoice>> GetAllWithProducts()
    {
        return await dbContext.Set<FactInvoice>()
            .Include(f => f.Client)
            .Include(f => f.IssueDate)
            .Include(f => f.DueDate)
            .Include(f=>f.Products)
            .ToListAsync();
    }
}