namespace MainBackend.Databases.BowlingDb.Entities;

public class Promotion : IEntity
{
    public int Id { get; set; }
    public DayOfWeek dayOfWeek { get; set; }
    public string description { get; set; }
}