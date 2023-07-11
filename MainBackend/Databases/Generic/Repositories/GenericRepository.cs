using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected readonly DbContext dbContext;
    
    protected GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<T> GetQuery()
    {
        return dbContext.Set<T>();
    }

    public T Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public bool Create(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Edit(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }
}