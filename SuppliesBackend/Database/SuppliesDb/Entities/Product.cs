using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Entities;

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual int OrderId { get; set; }
    public virtual Order Order { get; set; }
}