namespace MainBackend.Databases.BowlingDb.Entities;

public class Regulation : IEntity
{
    public int Id { get; set; }
    public int number { get; set; }
    public string description { get; set; }
}