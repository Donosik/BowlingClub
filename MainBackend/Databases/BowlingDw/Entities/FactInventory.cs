using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactInventory : IEntity
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    
    public DimProduct DimProduct { get; set; }
    public DimDate DimDate { get; set; }
}