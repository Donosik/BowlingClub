using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDw.Repositories.Interfaces;

public interface IFactInvoiceRepository : IGenericRepository<FactInvoice>
{
    Task<IEnumerable<FactInvoice>> GetAllWithDims();
    Task<IEnumerable<FactInvoice>> GetAllWithProducts();
}