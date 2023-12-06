using MainBackend.Databases.BowlingDw.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDw.Context;

public class BowlingDw : DbContext
{
    public DbSet<DimClient> dimClients { get; set; }
    public DbSet<DimDate> dimDates { get; set; }
    public DbSet<DimProduct> dimProducts { get; set; }
    public DbSet<DimWorker> dimWorkers { get; set; }
    public DbSet<FactInvoice> factInvoices { get; set; }
    public DbSet<FactReservation> factReservations { get; set; }
    public DbSet<FactWorkSchedule> factWorkSchedules { get; set; }

    public BowlingDw(DbContextOptions<BowlingDw> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FactInvoice>().Property(e => e.Amount).HasPrecision(38, 18);
        modelBuilder.Entity<FactInvoice>().HasOne<DimDate>(f => f.IssueDate).WithMany()
            .HasForeignKey(f => f.IssueDateId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<FactInvoice>().HasOne<DimDate>(f => f.DueDate).WithMany().HasForeignKey(f => f.DueDateId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FactReservation>().HasOne<DimDate>(f => f.StartTime).WithMany()
            .HasForeignKey(f => f.StartTimeId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<FactReservation>().HasOne<DimDate>(f => f.EndTime).WithMany()
            .HasForeignKey(f => f.EndTimeId).OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<FactWorkSchedule>().HasOne<DimDate>(f => f.WorkStart).WithMany()
            .HasForeignKey(f => f.WorkStartId).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<FactWorkSchedule>().HasOne<DimDate>(f => f.WorkEnd).WithMany()
            .HasForeignKey(f => f.WorkEndId).OnDelete(DeleteBehavior.NoAction);
        
        base.OnModelCreating(modelBuilder);
    }
}