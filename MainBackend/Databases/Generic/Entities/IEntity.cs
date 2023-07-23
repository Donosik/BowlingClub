using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public interface IEntity
{
#region Id

    [Key] int Id { get; set; }

#endregion
}