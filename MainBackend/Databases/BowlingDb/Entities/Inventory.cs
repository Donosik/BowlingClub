using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Inventory : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool isBar { get; set; }
    
    public virtual Invoice? Invoice { get; set; }
}