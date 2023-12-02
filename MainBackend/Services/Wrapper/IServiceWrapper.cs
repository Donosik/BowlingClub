using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IBarInventoryService barInventory { get; }
    IClientService client { get; }
    IDataService data { get; }
    IGeneratorService generator { get; }
    IInvoiceService invoice { get; }
    ILanesService lane { get; }
    IPersonService person { get; }
    IPromotionService promotion { get; }
    IRaportService raport { get; }
    IRegulationService regulation { get; }
    IReservationService reservation { get; }
    IUserService user { get; }
    IWorkerService worker { get; }
    IWorkScheduleService workSchedule { get; }
}