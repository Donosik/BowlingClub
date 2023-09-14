using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class FactReservation : IEntity
{
    public int Id { get; set; }
    public int laneNumber { get; set; }
    
    public virtual int StartTimeId { get; set; }
    public virtual DimDate StartTime { get; set; }
    public virtual int EndTimeId { get; set; }
    public virtual DimDate EndTime { get; set; }
    public virtual DimClient DimClient { get; set; }
}