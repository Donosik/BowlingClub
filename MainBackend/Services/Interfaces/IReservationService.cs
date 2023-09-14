using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IReservationService
{
#region Get

    Task<ICollection<Reservation>> GetReservations();

#endregion

#region Create

    Task<bool> MakeReservation(DateTime start,DateTime end,Client client);

#endregion

#region Delete

    Task<bool> DeleteReservation(int id);

#endregion
}