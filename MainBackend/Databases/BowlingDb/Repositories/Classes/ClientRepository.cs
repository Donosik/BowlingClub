using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Classes;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(Context.BowlingDb dbContext) : base(dbContext)
    {
        
    }
}