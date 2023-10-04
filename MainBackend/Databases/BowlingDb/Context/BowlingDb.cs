using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Context;

public class BowlingDb : DbContext
{
    public DbSet<BarInventory> BarInventories { get; set; }
    public DbSet<Client> Clients { get; set; }
    //public DbSet<InternalInventory> InternalInventories { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Lane> Lanes { get; set; }
    //public DbSet<NormalInventory> NormalInventories { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Worker> Workers { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }

#region Constructors

    public BowlingDb(DbContextOptions<BowlingDb> options) : base(options)
    {
    }

#endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Client and User relation 1:1
        modelBuilder.Entity<Client>()
            .HasOne<User>(c => c.User)
            .WithOne()
            .HasForeignKey<Client>(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        // Worker and User relation 1:1
        modelBuilder.Entity<Worker>()
            .HasOne<User>(w => w.User)
            .WithOne()
            .HasForeignKey<Worker>(w => w.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
    }
}