using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IPersonService
{
#region Get

    Task<ICollection<Person>> GetPersons();

#endregion

#region Delete

    Task<bool> DeletePerson(int id);

#endregion
}