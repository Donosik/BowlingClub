using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public interface IEntity
{
    [Key] int Id { get; set; }
}