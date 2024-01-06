using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.DTO;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IRaportService
{
    Task<IEnumerable<ClientWIthInvoices>> BestBuyingClient(int howManyDaysAgo, int howManyDaysForward, int howManyTop);

    Task<IEnumerable<InvoicesWithProducts>> BestSellingProducts(int howManyDaysAgo, int howManyDaysForward,
        int howManyTop);
}