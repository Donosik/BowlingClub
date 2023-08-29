using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class Person : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<User> Users { get; set; }
    public virtual Client? Client { get; set; }
    public virtual Worker? Worker { get; set; }
}