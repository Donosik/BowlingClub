using System.Collections.ObjectModel;
using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IClientService
{
    Task<ICollection<Client>> GetClients();

    Task<bool> DeletePerson(int id);
}