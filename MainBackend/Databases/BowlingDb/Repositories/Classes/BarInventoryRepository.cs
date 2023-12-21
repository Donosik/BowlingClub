using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class BarInventoryRepository : GenericRepository<Entities.Inventory>,IBarInventoryRepository
{
    public BarInventoryRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }
}