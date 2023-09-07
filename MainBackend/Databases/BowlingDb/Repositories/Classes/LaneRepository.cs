using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class LaneRepository : GenericRepository<Lane>, ILaneRepository
{
    public LaneRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }
}