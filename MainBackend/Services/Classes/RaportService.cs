using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class RaportService : IRaportService
{
    private IRepositoryWrapper repositoryWrapper;

    public RaportService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<IEnumerable<WorkerWithHours>> MostWorkedHours(int howManyDaysAgo,int howManyTop)
    {
        DateTime endDate = DateTime.Today;
        DateTime startDate = endDate.AddDays(-howManyDaysAgo);
        IEnumerable<FactWorkSchedule> workSchedules =
            await repositoryWrapper.normalDwWrapper.workSchedule.GetAllWithDims();

        var workersWithWorkHours = workSchedules
            .Where(work => work.WorkStart.CalendarDate >= startDate && work.WorkEnd.CalendarDate <= endDate)
            .GroupBy(work => work.Worker)
            .Select(group => new WorkerWithHours
            {
                Id = group.Key.Id,
                FullName = group.Key.FullName,
                Email = group.Key.Email,
                TotalWorkHours = group.Sum(work =>
                    Math.Max(0, (work.WorkEnd.CalendarDate - work.WorkStart.CalendarDate).TotalHours))
            }).OrderByDescending(worker => worker.TotalWorkHours)
            .ToList();

        return workersWithWorkHours;
    }

    public async Task<IEnumerable<ClientWIthInvoices>> BestBuyingClient(int howManyDaysAgo,int howManyTop)
    {
        DateTime endDate = DateTime.Today;
        DateTime startDate = endDate.AddDays(-howManyDaysAgo);
        IEnumerable<FactInvoice> invoices = await repositoryWrapper.normalDwWrapper.invoice.GetAllWithDims();

        var bestBuyingClients = invoices
            .Where(invoice => invoice.IssueDate.CalendarDate >= startDate && invoice.IssueDate.CalendarDate <= endDate)
            .GroupBy(invoice => invoice.Client)
            .Select(group => new ClientWIthInvoices
            {
                Id = group.Key.Id,
                FullName = group.Key.FullName,
                Email = group.Key.Email,
                TotalMoneySpend = group.Sum(invoice=>invoice.Amount)
            })
            .OrderByDescending(client => client.TotalMoneySpend)
            .ToList();

        return bestBuyingClients;
    }

    public async Task<IEnumerable<InvoicesWithProducts>> BestSellingProducts(int howManyDaysAgo,int howManyTop)
    {
        DateTime endDate = DateTime.Today;
        DateTime startDate = endDate.AddDays(-howManyDaysAgo);
        IEnumerable<FactInvoice> invoices = await repositoryWrapper.normalDwWrapper.invoice.GetAllWithProducts();

        var bestSellingProducts = invoices
            .Where(invoice => invoice.IssueDate.CalendarDate >= startDate && invoice.IssueDate.CalendarDate <= endDate)
            .SelectMany(invoice=>invoice.Products)
            .GroupBy(product => product.ProductName)
            .Select(group => new InvoicesWithProducts
            {
                Id = group.First().Id,
                ProductName = group.Key,
                TotalSold=group.Count()
            })
            .OrderByDescending(client => client.TotalSold)
            .ToList();

        return bestSellingProducts;
    }
}