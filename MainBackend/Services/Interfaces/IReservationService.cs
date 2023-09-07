using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IReservationService
{
#region Get

    Task<ICollection<Reservation>> GetReservations();

#endregion

#region Create

    Task<bool> MakeReservation();

#endregion

#region Delete

    Task<bool> DeleteReservation(int id);

#endregion
}