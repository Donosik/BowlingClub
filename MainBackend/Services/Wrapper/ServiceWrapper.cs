using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IUserService user { get; }
    public IPersonService person { get; }
    public IGeneratorService generator { get; }
    public IWorkScheduleService workSchedule { get; }
    public ILanesService lane { get; }

#region Constructors

    public ServiceWrapper(IUserService user, IPersonService person,IGeneratorService generator,IWorkScheduleService workSchedule,ILanesService lane)
    {
        this.user = user;
        this.person = person;
        this.generator = generator;
        this.workSchedule = workSchedule;
        this.lane = lane;
    }

#endregion
}