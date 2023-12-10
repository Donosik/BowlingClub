using SuppliesBackend.Database.Generic.Entities;

namespace SuppliesBackend.Database.Generic.Repositories;

public interface IGenericRepository<T> where T : class, IEntity
{
#region Get

    Task<T> Get(int id);

#endregion

#region GetAll

    Task<ICollection<T>> GetAll();

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