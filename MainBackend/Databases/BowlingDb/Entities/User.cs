using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public class User : IEntity
{
    #region Id

    [Key]
    public int Id { get; set; }

    #endregion

    #region Variables

    public int Login { get; set; }

    #endregion

    #region Constructors

    public User()
    {
        Login = 0;
    }

    #endregion
}