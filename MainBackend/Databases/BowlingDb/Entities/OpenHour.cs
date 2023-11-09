using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.DTO;

public class OpenHour : IEntity
{
    public int Id { get; set; }
    public DayOfWeek dayOfWeek { get; set; }
    public TimeSpan startTime { get; set; }
    public TimeSpan endTime { get; set; }
}