using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    [Key] public int Id { get; set; }
    public string Login { get; set; }


#region Constructors

    public User()
    {
        Login = "";
    }

#endregion
}