using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface ILanesService
{
#region Get

    Task<ICollection<Lane>> GetLanes();

#endregion

#region Create

    Task<bool> AddLane(int laneNumber);

#endregion

#region Delete

    Task<bool> DeleteLane(int id);

#endregion
}