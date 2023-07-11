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

    public async Task<T> Get(int id)
    {
        return await GetQuery().FirstOrDefaultAsync(t=>t.Id==id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await GetQuery().ToListAsync();
    }

    public void Create(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }

    public void Edit(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public async Task Delete(int id)
    {
        T entity = await Get(id);
        Delete(entity);
    }
}