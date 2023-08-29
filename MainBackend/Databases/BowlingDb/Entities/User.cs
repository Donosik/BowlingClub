using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    
    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsClient { get; set; }
    
    public virtual Person Person { get; set; }
}