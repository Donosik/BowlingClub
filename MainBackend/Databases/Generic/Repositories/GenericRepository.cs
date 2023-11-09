using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.Generic.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    protected readonly DbContext dbContext;

#region Constructors

    protected GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

#endregion

    protected IQueryable<T> GetQuery()
    {
        return dbContext.Set<T>();
    }

#region Get

    public async Task<T> Get(int id)
    {
        return await GetQuery().FirstOrDefaultAsync(t => t.Id == id);
    }

#endregion

#region GetAll

    public async Task<ICollection<T>> GetAll()
    {
        return await GetQuery().ToListAsync();
    }

#endregion

#region Create

    public void Create(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }

#endregion

#region Edit

    public void Edit(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }

#endregion

#region Delete

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public async Task Delete(int id)
    {
        T entity = await Get(id);
        Delete(entity);
    }

#endregion
}