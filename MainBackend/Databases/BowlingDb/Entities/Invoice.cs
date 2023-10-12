using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Invoice : IEntity
{
    public int Id { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    //public float Amount { get; set; }
    
    public virtual int ClientId { get; set; }
    public virtual Client Client { get; set; }
    public virtual int WorkerId { get; set; }
    public virtual Worker Worker { get; set; }
    public virtual ICollection<BarInventory> BarInventories { get; set; }

    public Invoice()
    {
        BarInventories = new List<BarInventory>();
    }
}