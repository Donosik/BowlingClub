using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    [Key] public int Id { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }


#region Constructors

    public User()
    {
        login = "";
    }

#endregion
}