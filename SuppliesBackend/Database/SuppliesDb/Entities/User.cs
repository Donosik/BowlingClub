using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.SuppliesDb.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Order> FullfilledOrders { get; set; }
}