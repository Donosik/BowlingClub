using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class WorkSchedule : IEntity
{
    public int Id { get; set; }

    public DateTime WorkStart { get; set; }
    public DateTime WorkEnd { get; set; }

    public virtual ICollection<Worker> Worker { get; set; }

    public WorkSchedule()
    {
        Worker = new List<Worker>();
    }
}