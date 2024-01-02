using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Invoice : IEntity
{
    public int Id { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public virtual int ClientId { get; set; }
    public virtual Client Client { get; set; }
    public virtual int WorkerId { get; set; }
    public virtual Worker Worker { get; set; }
    public virtual int? ReservationId { get; set; }
    public virtual Reservation? Reservation { get; set; }
    public virtual ICollection<Inventory> Inventories { get; set; }

    public Invoice()
    {
        Inventories = new List<Inventory>();
    }
}