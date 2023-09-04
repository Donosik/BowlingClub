using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class WorkerRepository : GenericRepository<Worker>,IWorkerRepository
{
    public WorkerRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }
}