using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactWorkSchedule : IEntity
{
    public int Id { get; set; }

    public virtual DimWorker Worker { get; set; }
    public virtual int WorkStartId { get; set; }
    public virtual DimDate WorkStart { get; set; }
    public virtual int WorkEndId { get; set; }
    public virtual DimDate WorkEnd { get; set; }
}