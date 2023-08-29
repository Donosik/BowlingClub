using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class NormalInventory : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
}