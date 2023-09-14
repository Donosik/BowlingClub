using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IClientService client { get; }
    public IGeneratorService generator { get; }
    public ILanesService lane { get; }
    public IPersonService person { get; }
    public IReservationService reservation { get; }
    public IUserService user { get; }
    public IWorkerService worker { get; }
    public IWorkScheduleService workSchedule { get; }

#region Constructors

    public ServiceWrapper(IUserService user, IPersonService person, IGeneratorService generator,
        IWorkScheduleService workSchedule, ILanesService lane, IWorkerService worker, IReservationService reservation,IClientService client)
    {
        this.user = user;
        this.person = person;
        this.generator = generator;
        this.workSchedule = workSchedule;
        this.lane = lane;
        this.worker = worker;
        this.reservation = reservation;
        this.client = client;
    }

#endregion
}