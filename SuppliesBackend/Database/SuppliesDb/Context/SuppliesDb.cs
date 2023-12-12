using Microsoft.EntityFrameworkCore;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Context;

public class SuppliesDb :DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public SuppliesDb(DbContextOptions<SuppliesDb> options) : base(options)
    {
    }
}