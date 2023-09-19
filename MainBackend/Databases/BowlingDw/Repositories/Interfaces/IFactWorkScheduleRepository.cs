using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDw.Repositories.Interfaces;

public interface IFactWorkScheduleRepository : IGenericRepository<FactWorkSchedule>
{
    Task<IEnumerable<FactWorkSchedule>> GetAllWithDims();
}