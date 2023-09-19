using MainBackend.Databases.BowlingDw.Entities;
using MainBackend.Databases.BowlingDw.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDw.Repositories.Classes;

public class FactWorkScheduleRepository : GenericRepository<FactWorkSchedule>,IFactWorkScheduleRepository
{
    public FactWorkScheduleRepository(Context.BowlingDw dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<FactWorkSchedule>> GetAllWithDims()
    {
        return await dbContext.Set<FactWorkSchedule>()
            .Include(f => f.WorkEnd)
            .Include(f => f.WorkStart)
            .Include(f => f.Worker)
            .ToListAsync();
    }
}