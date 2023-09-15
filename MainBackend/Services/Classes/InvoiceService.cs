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

    public async Task<bool> AddInvoice(ICollection<BarInventory> products, Client client, DateTime issueDate,
        DateTime dueDate)
    {
        Invoice invoice = new Invoice();
        invoice.Client = client;
        invoice.IssueDate = issueDate;
        invoice.DueDate = dueDate;
        invoice.BarInventories = products;
        repositoryWrapper.normalDbWrapper.invoice.Create(invoice);
        repositoryWrapper.normalDbWrapper.client.Edit(client);
        int entities = 2;
        foreach (var product in products)
        {
            repositoryWrapper.normalDbWrapper.barInventory.Edit(product);
            entities++;
        }
        return await repositoryWrapper.normalDbWrapper.Save(entities);
    }
}