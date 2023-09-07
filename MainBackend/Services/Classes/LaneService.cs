using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class LaneService : ILanesService
{
    private IRepositoryWrapper repositoryWrapper;

    public LaneService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<Lane>> GetLanes()
    {
        IEnumerable<Lane> lanes = await repositoryWrapper.normalDbWrapper.lane.GetAll();
        return (ICollection<Lane>)lanes;
    }

    public async Task<bool> AddLane(int laneNumber)
    {
        Lane lane = new Lane();
        lane.LaneNumber = laneNumber;
        repositoryWrapper.normalDbWrapper.lane.Create(lane);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> DeleteLane(int id)
    {
        if (id <= 0)
            return false;
        Lane lane = await repositoryWrapper.normalDbWrapper.lane.Get(id);
        if (lane == null)
            return false;
        await repositoryWrapper.normalDbWrapper.lane.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}