using MainBackend.Databases.BowlingDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    #region Variables

    protected readonly DbContext dbContext;

    #endregion
    

    #region Constructors

    protected GenericRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #endregion

    public IQueryable<T> GetQuery()
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

    public async Task<IEnumerable<T>> GetAll()
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