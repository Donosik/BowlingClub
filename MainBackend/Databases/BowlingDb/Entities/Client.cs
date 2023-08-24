namespace MainBackend.Databases.BowlingDb.Entities;

public class Client : IEntity
{
    public int id { get; set; }
    
    public virtual int personId { get; set; }
    public virtual Person person { get; set; }
    public virtual ICollection<Reservation> reservations { get; set; }
    public virtual ICollection<Invoice> invoices { get; set; }
}