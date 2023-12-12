using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Entities;

public class Order : IEntity
{
    public int Id { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public bool IsFulfilled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}