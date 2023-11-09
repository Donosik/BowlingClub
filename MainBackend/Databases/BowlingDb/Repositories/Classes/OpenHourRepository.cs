using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class OpenHourRepository : GenericRepository<OpenHour>,IOpenHourRepository
{
    public OpenHourRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }
}