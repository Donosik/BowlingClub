using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactWorkSchedule : IEntity
{
    public int Id { get; set; }
    public DateTime WorkStart { get; set; }
    public DateTime WorkEnd { get; set; }

    public DimWorker DimWorker { get; set; }
    public DimDate DimDate { get; set; }
}