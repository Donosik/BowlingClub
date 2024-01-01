using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<ICollection<Invoice>> GetWorkerInvoices(int workerId);
    Task<ICollection<Invoice>> GetClientInvoices(int clientId);
    Task<ICollection<Invoice>> GetAllWithUsers();
}