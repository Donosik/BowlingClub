using MainBackend.Databases.BowlingDw.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDw.Context;

public class BowlingDw : DbContext
{
    public DbSet<DimClient> dimClients { get; set; }
    public DbSet<DimDate> dimDates { get; set; }
    public DbSet<DimProduct> dimProducts { get; set; }
    public DbSet<DimWorker> dimWorkers { get; set; }
    public DbSet<FactInventory> factInventories { get; set; }
    public DbSet<FactInvoice> factInvoices { get; set; }
    public DbSet<FactReservation> factReservations { get; set; }
    public DbSet<FactWorkSchedule> factWorkSchedules { get; set; }

    public BowlingDw(DbContextOptions<BowlingDw> options) : base(options)
    {
    }
}