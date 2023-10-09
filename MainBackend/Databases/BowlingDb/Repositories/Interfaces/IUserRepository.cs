using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUser(string login);
    Task<User> GetWorker(int id);
}