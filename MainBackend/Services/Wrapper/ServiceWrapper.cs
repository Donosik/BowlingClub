using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IBarInventoryService barInventory { get; }
    public IClientService client { get; }
    public IDataService data { get; }
    public IGeneratorService generator { get; }
    public IInvoiceService invoice { get; }
    public ILanesService lane { get; }
    public IPersonService person { get; }
    public IPromotionService promotion { get; }
    public IRaportService raport { get; }
    public IRegulationService regulation { get; }
    public IReservationService reservation { get; }
    public ISupplyService supply { get; }
    public IUserService user { get; }
    public IWorkerService worker { get; }
    public IWorkScheduleService workSchedule { get; }

#region Constructors

    public ServiceWrapper(IUserService user, IPersonService person, IGeneratorService generator,
        IWorkScheduleService workSchedule, ILanesService lane, IWorkerService worker, IReservationService reservation,
        IClientService client, IInvoiceService invoice, IBarInventoryService barInventory, IRaportService raport,
        IDataService data,IRegulationService regulation,IPromotionService promotion,ISupplyService supply)
    {
        this.user = user;
        this.person = person;
        this.generator = generator;
        this.workSchedule = workSchedule;
        this.lane = lane;
        this.worker = worker;
        this.reservation = reservation;
        this.client = client;
        this.invoice = invoice;
        this.barInventory = barInventory;
        this.raport = raport;
        this.data = data;
        this.promotion = promotion;
        this.regulation = regulation;
        this.supply = supply;
    }

#endregion
}