using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
    }

    public async Task<Person> GetPerson(string email)
    {
        return await dbContext.Set<Person>().FirstOrDefaultAsync(p => p.Email == email);
    }
}