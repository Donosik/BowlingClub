using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Reservation : IEntity
{
    public int Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    
    public virtual Invoice? Invoice { get; set; }
    public virtual Lane Lane { get; set; }
    public virtual Client Client { get; set; }
}