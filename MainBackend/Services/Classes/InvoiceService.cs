using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;

namespace MainBackend.Services.Classes;

public class InvoiceService : IInvoiceService
{
    private IRepositoryWrapper repositoryWrapper;

    public InvoiceService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<Invoice>> GetInvoices()
    {
        return await repositoryWrapper.normalDbWrapper.invoice.GetAll();
    }

    public async Task<bool> AddInvoice(ICollection<Inventory> products, Client client, Worker worker,
        DateTime issueDate,
        DateTime dueDate)
    {
        Invoice invoice = new Invoice();
        invoice.Client = client;
        invoice.Worker = worker;
        invoice.IssueDate = issueDate;
        invoice.DueDate = dueDate;
        invoice.Inventories = products;
        decimal value = 0;
        foreach (var product in products)
        {
            value += product.Price;
        }

        invoice.Amount = value;
        repositoryWrapper.normalDbWrapper.invoice.Create(invoice);
        repositoryWrapper.normalDbWrapper.client.Edit(client);
        repositoryWrapper.normalDbWrapper.worker.Edit(worker);
        int entities = 3;
        foreach (var product in products)
        {
            repositoryWrapper.normalDbWrapper.barInventory.Edit(product);
            entities++;
        }

        return await repositoryWrapper.normalDbWrapper.Save(entities);
    }

    public async Task<bool> DeleteInvoice(int id)
    {
        Invoice invoice = await repositoryWrapper.normalDbWrapper.invoice.Get(id);
        if (invoice == null)
            return false;
        repositoryWrapper.normalDbWrapper.invoice.Delete(invoice);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}