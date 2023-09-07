using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IWorkScheduleService
{
#region Get

    Task<ICollection<WorkSchedule>> GetWorkSchedules();

#endregion

#region Create

    Task<bool> AddShift(Worker worker, DateTime start, DateTime end);

#endregion

#region Delete

    Task<bool> DeleteShift(int id);

#endregion
}