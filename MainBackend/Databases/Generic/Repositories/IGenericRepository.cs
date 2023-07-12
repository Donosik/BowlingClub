using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Databases.Generic.Repositories;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetQuery();

    #region Get

    Task<T> Get(int id);

    #endregion

    #region GetAll

    Task<IEnumerable<T>> GetAll();

    #endregion

    #region Create

    void Create(T entity);

    #endregion

    #region Edit

    void Edit(T entity);

    #endregion

    #region Delete

    void Delete(T entity);
    Task Delete(int id);

    #endregion
}