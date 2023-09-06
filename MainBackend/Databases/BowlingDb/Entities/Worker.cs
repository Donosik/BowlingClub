namespace MainBackend.Databases.BowlingDb.Entities;

public class Worker  : IEntity
{
    public int Id { get; set; }
    
    public virtual int PersonId { get; set; }
    public virtual Person Person { get; set; }
    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }

    public Worker()
    {
        WorkSchedules = new List<WorkSchedule>();
    }
}