using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IInvoiceService
{
    Task<ICollection<Invoice>> GetInvoices();
    Task<bool> AddInvoice(ICollection<Inventory> products,Client client,Worker worker,DateTime issueDate,DateTime dueDate);
    Task<bool> DeleteInvoice(int id);
}