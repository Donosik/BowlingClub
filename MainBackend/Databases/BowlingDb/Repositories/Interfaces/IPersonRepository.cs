using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IPersonRepository : IGenericRepository<Person>
{
    Task<Person> GetPerson(string email);
}