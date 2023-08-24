using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Reservation : IEntity
{
    public int id { get; set; }
    
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    
    public virtual Lane lane { get; set; }
    public virtual Client client { get; set; }
}