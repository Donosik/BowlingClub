using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Person : IEntity
{
    public int id { get; set; }
    
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    
    public virtual ICollection<User> users { get; set; }
    public virtual Client? client { get; set; }
    public virtual Worker? worker { get; set; }
}