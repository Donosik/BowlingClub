using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
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
        return await repositoryWrapper.normalDbWrapper.invoice.GetAllWithUsers();
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
    
    public async Task<bool> AddInvoice(ICollection<Inventory> products, Client client, Worker worker,
        DateTime issueDate,
        DateTime dueDate,Reservation reservation)
    {
        Invoice invoice = new Invoice();
        invoice.Client = client;
        invoice.Worker = worker;
        invoice.IssueDate = issueDate;
        invoice.DueDate = dueDate;
        invoice.Inventories = products;
        invoice.Reservation = reservation;
        decimal value = 0;
        foreach (var product in products)
        {
            value += product.Price;
        }

        invoice.Amount = value;
        repositoryWrapper.normalDbWrapper.invoice.Create(invoice);
        repositoryWrapper.normalDbWrapper.client.Edit(client);
        repositoryWrapper.normalDbWrapper.worker.Edit(worker);
        repositoryWrapper.normalDbWrapper.reservation.Edit(reservation);
        int entities = 4;
        foreach (var product in products)
        {
            repositoryWrapper.normalDbWrapper.barInventory.Edit(product);
            entities++;
        }

        return await repositoryWrapper.normalDbWrapper.Save(entities);
    }

    public async Task<bool> AddInvoice(InvoiceForm invoiceForm, int workerId)
    {
        ICollection<Inventory> products = new List<Inventory>();
        foreach (var product in invoiceForm.Products)
        {
                ICollection<Inventory> inventory =
                    await repositoryWrapper.normalDbWrapper.barInventory.GetByProductName(product.Name,product.Quantity);
                if (inventory == null)
                    return false;
                foreach (var item in inventory)
                {
                    products.Add(item);
                }
        }
        User userClient = await repositoryWrapper.normalDbWrapper.user.Get(invoiceForm.ClientUserId);
        Client client = userClient.Person.Client;
        User userWorker= await repositoryWrapper.normalDbWrapper.user.Get(workerId);
        Worker worker = userWorker.Person.Worker;
        DateTime dueDate = invoiceForm.PayingDate;
        DateTime issueDate = DateTime.Now;
        return await AddInvoice(products, client, worker, issueDate, dueDate);
    }
    
    public async Task<bool> AddInvoice(InvoiceForm invoiceForm, int workerId,int reservationId)
    {
        var reservation = await repositoryWrapper.normalDbWrapper.reservation.Get(reservationId);
        if (reservation == null)
            return false;
        ICollection<Inventory> products = new List<Inventory>();
        foreach (var product in invoiceForm.Products)
        {
            ICollection<Inventory> inventory =
                await repositoryWrapper.normalDbWrapper.barInventory.GetByProductName(product.Name,product.Quantity);
            if (inventory == null)
                return false;
            foreach (var item in inventory)
            {
                products.Add(item);
            }
        }
        User userClient = await repositoryWrapper.normalDbWrapper.user.Get(invoiceForm.ClientUserId);
        Client client = userClient.Person.Client;
        User userWorker= await repositoryWrapper.normalDbWrapper.user.Get(workerId);
        Worker worker = userWorker.Person.Worker;
        DateTime dueDate = invoiceForm.PayingDate;
        DateTime issueDate = DateTime.Now;
        return await AddInvoice(products, client, worker, issueDate, dueDate,reservation);
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