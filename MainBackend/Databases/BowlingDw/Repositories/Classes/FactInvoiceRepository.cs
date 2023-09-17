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
}