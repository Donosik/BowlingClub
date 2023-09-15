using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IBarInventoryService barInventory { get; }
    public IClientService client { get; }
    public IGeneratorService generator { get; }
    public IInvoiceService invoice { get; }
    public ILanesService lane { get; }
    public IPersonService person { get; }
    public IRaportService raport { get; }
    public IReservationService reservation { get; }
    public IUserService user { get; }
    public IWorkerService worker { get; }
    public IWorkScheduleService workSchedule { get; }

#region Constructors

    public ServiceWrapper(IUserService user, IPersonService person, IGeneratorService generator,
        IWorkScheduleService workSchedule, ILanesService lane, IWorkerService worker, IReservationService reservation,
        IClientService client, IInvoiceService invoice, IBarInventoryService barInventory, IRaportService raport)
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
    }

#endregion
}