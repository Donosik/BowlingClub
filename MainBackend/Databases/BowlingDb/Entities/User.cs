using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
#region Variables

    [Key] public int Id { get; set; }
    public string Login { get; set; }
    

#endregion

#region Constructors

    public User()
    {
        Login = "";
    }

#endregion
}