using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetQuery();
    Task<T> Get(int id);

    Task<IEnumerable<T>> GetAll();

    void Create(T entity);

    void Edit(T entity);

    void Delete(T entity);
    Task Delete(int id);
}