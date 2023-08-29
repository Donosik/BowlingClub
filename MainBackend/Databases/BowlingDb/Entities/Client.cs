namespace MainBackend.Databases.BowlingDb.Entities;

public class Client : IEntity
{
    public int Id { get; set; }
    
    public virtual int PersonId { get; set; }
    public virtual Person Person { get; set; }
    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
}