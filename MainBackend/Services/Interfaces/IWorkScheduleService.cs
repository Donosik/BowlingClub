using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IWorkScheduleService
{

#region Create

    Task<bool> AddShift(Worker worker, DateTime start, DateTime end);

#endregion
}