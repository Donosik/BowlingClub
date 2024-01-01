using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IReservationService
{
#region Get

    Task<ICollection<Reservation>> GetReservations();
    Task<ICollection<Reservation>> GetReservationsByClient(int clientId);

#endregion

#region Create

    Task<bool> MakeReservation(DateTime start,DateTime end,Client client);
    Task<bool> MakeReservation(ReservationForm reservationForm, int clientId);

#endregion

#region Delete

    Task<bool> DeleteReservation(int id);

#endregion
}