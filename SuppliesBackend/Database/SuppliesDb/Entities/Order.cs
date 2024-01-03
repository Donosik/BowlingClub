using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Entities;

public class Order : IEntity
{
    public int Id { get; set; }
    public virtual ICollection<Product> Products { get; set; }
    public bool IsFullfilled { get; set; } = false;
    public bool IsTaken { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public virtual User? User { get; set; }
}