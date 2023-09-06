using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class DimDate : IEntity
{
    public int Id { get; set; }
    public DateTime CalendarDate { get; set; }
}