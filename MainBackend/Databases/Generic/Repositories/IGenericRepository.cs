using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetQuery();

    T Get(int id);

    IEnumerable<T> GetAll();

    bool Create(T entity);

    bool Edit(T entity);

    bool Delete(T entity);
    bool Delete(int id);
}