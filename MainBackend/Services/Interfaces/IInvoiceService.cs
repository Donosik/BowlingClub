using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IInvoiceService
{
    Task<bool> AddInvoice(ICollection<BarInventory> products,Client client,DateTime issueDate,DateTime dueDate);
}