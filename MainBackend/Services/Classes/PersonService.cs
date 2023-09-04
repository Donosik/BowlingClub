using System.Collections;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class PersonService : IPersonService
{
    private IRepositoryWrapper repositoryWrapper;

#region Constructors

    public PersonService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

#endregion

#region Get

    public async Task<ICollection<Person>> GetPersons()
    {
        IEnumerable<Person> persons = await repositoryWrapper.normalDbWrapper.person.GetAll();
        return (ICollection<Person>)persons;
    }

#endregion

#region Delete

    public async Task<bool> DeletePerson(int id)
    {
        if (id <= 0)
            return false;
        Person person=await repositoryWrapper.normalDbWrapper.person.Get(id);
        if (person == null)
            return false;
        await repositoryWrapper.normalDbWrapper.person.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

#endregion
}