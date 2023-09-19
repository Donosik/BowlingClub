using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.DTO;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IRaportService
{
    Task<IEnumerable<WorkerWithHours>> MostWorkedHours(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ClientWIthInvoices>> BestBuyingClient(DateTime startDate, DateTime endDate);
    Task<IEnumerable<InvoicesWithProducts>> BestSellingProducts(DateTime startDate, DateTime endDate);
}