namespace MainBackend.Databases.BowlingDb.Entities;

public class TargetInventory : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}