using MainBackend.Databases.BowlingDb.Repositories.Interfaces;

namespace MainBackend.Databases.BowlingDb.RepositoryWrapper;

public class RepositoryWrapperDb : IRepositoryWrapperDb
{
    public IClientRepository client { get; }
    public ILaneRepository lane { get; }
    public IPersonRepository person { get; }
    public IReservationRepository reservation { get; }
    public IUserRepository user { get; }
    public IWorkerRepository worker { get; }
    public IWorkScheduleRepository workSchedule { get; }
    private readonly Context.BowlingDb dbContext;

#region Constructors

    public RepositoryWrapperDb(Context.BowlingDb dbContext, IUserRepository user, IPersonRepository person,
        IClientRepository client, IWorkerRepository worker, IWorkScheduleRepository workSchedule, ILaneRepository lane,IReservationRepository reservation)
    {
        this.dbContext = dbContext;
        this.user = user;
        this.person = person;
        this.client = client;
        this.worker = worker;
        this.workSchedule = workSchedule;
        this.lane = lane;
        this.reservation = reservation;
    }

#endregion

    public async Task<bool> Save(int entities = 1)
    {
        int result = await dbContext.SaveChangesAsync();
        if (result >= entities)
            return true;
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            dbContext.Dispose();
    }
}