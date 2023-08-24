using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    public int id { get; set; }
    
    public string login { get; set; }
    public string password { get; set; }
    public bool isClient { get; set; }
    
    public virtual Person person { get; set; }
}