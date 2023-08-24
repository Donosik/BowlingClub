namespace MainBackend.Databases.BowlingDb.Entities;

public class Worker  : IEntity
{
    public int id { get; set; }
    
    public virtual int personId { get; set; }
    public virtual Person person { get; set; }
    public virtual ICollection<WorkSchedule> workSchedules { get; set; }
    //public virtual ICollection<Invoice> invoices { get; set; }
}