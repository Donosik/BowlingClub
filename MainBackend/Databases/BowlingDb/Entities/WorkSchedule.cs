using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class WorkSchedule : IEntity
{
    public int id { get; set; }
    
    public DateTime workStart { get; set; }
    public DateTime workEnd { get; set; }
    
    public virtual Worker worker { get; set; }
    
}