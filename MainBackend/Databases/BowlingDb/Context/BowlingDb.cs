using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Context;

public class BowlingDb : DbContext
{
    public DbSet<User> Users { get; set; }

#region Constructors

    public BowlingDb(DbContextOptions<BowlingDb> options) : base(options)
    {
    }

#endregion
}