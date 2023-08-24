using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Context;

public class BowlingDb : DbContext
{
    public DbSet<BarInventory> barInventories { get; set; }
    public DbSet<Client> clients { get; set; }
    public DbSet<InternalInventory> internalInventories { get; set; }
    public DbSet<Invoice> invoices { get; set; }
    public DbSet<Lane> lanes { get; set; }
    public DbSet<NormalInventory> normalInventories { get; set; }
    public DbSet<Person> persons { get; set; }
    public DbSet<Reservation> reservations { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Worker> workers { get; set; }
    public DbSet<WorkSchedule> workSchedules { get; set; }

#region Constructors

    public BowlingDb(DbContextOptions<BowlingDb> options) : base(options)
    {
    }

#endregion
}