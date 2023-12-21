using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
    IBarInventoryRepository barInventory { get; }
    IClientRepository client { get; }
    IInvoiceRepository invoice { get; }
    ILaneRepository lane { get; }
    IOpenHourRepository openHour { get; }
    IPersonRepository person { get; }
    IPromotionRepository promotion { get; }
    IRegulationRepository regulation { get; }
    IReservationRepository reservation { get; }
    ITargetInventoryRepository targetInventory { get; }
    IUserRepository user { get; }
    IWorkerRepository worker { get; }
    IWorkScheduleRepository workSchedule { get; }
    
#region Methods

    Task<bool> Save(int entities = 1);
    void Dispose();

#endregion
}