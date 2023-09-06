using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class DimProduct : IEntity
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
}