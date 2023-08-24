namespace MainBackend.Databases.BowlingDb.Entities;

public class Lane : IEntity
{
    public int id { get; set; }
    
    public int laneNumber { get; set; }
    
    public virtual ICollection<Reservation> reservations { get; set; }
}