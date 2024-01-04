using Microsoft.EntityFrameworkCore;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Context;

public class SuppliesDb : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    public SuppliesDb(DbContextOptions<SuppliesDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.FullfilledOrders)
            .HasForeignKey(x=>x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(x=>x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}