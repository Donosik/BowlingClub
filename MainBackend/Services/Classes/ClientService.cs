using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class ClientService : IClientService
{
    private IRepositoryWrapper repositoryWrapper;

    public ClientService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }
    
    public async Task<ICollection<Client>> GetClients()
    {
        IEnumerable<Client> clients = await repositoryWrapper.normalDbWrapper.client.GetAll();
        return (ICollection<Client>)clients;
    }

    public async Task<bool> DeletePerson(int id)
    {
        if (id <= 0)
            return false;
        Client client = await repositoryWrapper.normalDbWrapper.client.Get(id);
        if (client == null)
            return false;
        await repositoryWrapper.normalDbWrapper.client.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}