namespace MainBackend.Databases.BowlingDb.Entities;

public class Lane : IEntity
{
    public int Id { get; set; }
    
    public int LaneNumber { get; set; }
    
    public virtual ICollection<Reservation> Reservations { get; set; }

    public Lane()
    {
        Reservations = new List<Reservation>();
    }
}