using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IInvoiceService
{
    Task<ICollection<Invoice>> GetInvoices();
    Task<bool> AddInvoice(ICollection<Inventory> products,Client client,Worker worker,DateTime issueDate,DateTime dueDate);
    Task<bool> AddInvoice(ICollection<Inventory> products,Client client,Worker worker,DateTime issueDate,DateTime dueDate,Reservation reservation);
    Task<bool> AddInvoice(InvoiceForm invoiceForm, int workerId);
    Task<bool> AddInvoice(InvoiceForm invoiceForm, int workerId,int reservationId);
    Task<bool> DeleteInvoice(int id);
}