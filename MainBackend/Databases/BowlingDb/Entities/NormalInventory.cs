using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class NormalInventory : IEntity
{
    public int id { get; set; }
    
    public string name { get; set; }
    public int quantity { get; set; }
    public float price { get; set; }
}