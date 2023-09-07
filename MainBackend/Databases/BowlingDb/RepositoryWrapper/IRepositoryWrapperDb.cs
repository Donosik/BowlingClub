using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public interface IRepositoryWrapperDb : IDisposable
{
    IClientRepository client { get; }
    ILaneRepository lane { get; }
    IPersonRepository person { get; }
    IReservationRepository reservation { get; }
    IUserRepository user { get; }
    IWorkerRepository worker { get; }
    IWorkScheduleRepository workSchedule { get; }
    
#region Methods

    Task<bool> Save(int entities = 1);
    void Dispose();

#endregion
}