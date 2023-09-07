using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class ReservationService : IReservationService
{
    private IRepositoryWrapper repositoryWrapper;

    public ReservationService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<Reservation>> GetReservations()
    {
        IEnumerable<Reservation> reservations = await repositoryWrapper.normalDbWrapper.reservation.GetAll();
        return (ICollection<Reservation>)reservations;
    }

    public async Task<bool> MakeReservation(DateTime start,DateTime end,Lane lane,Client client)
    {
        //TODO: Sprawdzenie dorobic
        
        Reservation reservation = new Reservation();
        reservation.StartTime = start;
        reservation.EndTime = end;
        reservation.Lane = lane;
        reservation.Client = client;
        repositoryWrapper.normalDbWrapper.reservation.Create(reservation);
        repositoryWrapper.normalDbWrapper.client.Edit(client);
        repositoryWrapper.normalDbWrapper.lane.Edit(lane);
        return await repositoryWrapper.normalDbWrapper.Save(3);
    }

    public async Task<bool> DeleteReservation(int id)
    {
        if (id <= 0)
            return false;
        Reservation reservation = await repositoryWrapper.normalDbWrapper.reservation.Get(id);
        if (reservation == null)
            return false;
        await repositoryWrapper.normalDbWrapper.reservation.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}