using System.ComponentModel.DataAnnotations;

namespace MainBackend.Databases.BowlingDb.Entities;

public interface IEntity
{
    int id { get; set; }
}