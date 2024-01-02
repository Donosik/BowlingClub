using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IReservationService
{
#region Get

    Task<ICollection<Reservation>> GetReservations();
    Task<ICollection<Reservation>> GetReservations(int usersPerPage,int currentPage);
    Task<ICollection<Reservation>> GetReservationsByClient(int clientId);
    Task<ICollection<Reservation>> GetReservationsByClient(int clientId,int usersPerPage,int currentPage);

#endregion

#region Create

    Task<bool> MakeReservation(DateTime start,DateTime end,Client client);
    Task<bool> MakeReservation(ReservationForm reservationForm, int clientId);

#endregion

#region Delete

    Task<bool> DeleteReservation(int id);

#endregion
}