using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;

namespace MainBackend.Databases.BowlingDb.Repositories.Interfaces;

public interface IReservationRepository : IGenericRepository<Reservation>
{
    Task<ICollection<Reservation>> GetAllOfClient(Client client);
    Task<ICollection<Reservation>> GetAllOfClient(Client client,int usersPerPage,int currentPage,bool onlyNew,bool onlyWithoutInvoice);
    Task<ICollection<Reservation>> GetAllWithIncludes();
    Task<ICollection<Reservation>> GetAllWithIncludes(int usersPerPage,int currentPage,bool onlyNew,bool onlyWithoutInvoice);
}