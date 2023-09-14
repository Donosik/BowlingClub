using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDw.Entities;

public class DimWorker : IEntity
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    
    public virtual DimDate DateOfBirth { get; set; }
}