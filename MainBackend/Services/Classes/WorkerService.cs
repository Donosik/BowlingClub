using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class WorkerService : IWorkerService
{
    private IRepositoryWrapper repositoryWrapper;

    public WorkerService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<Worker>> GetWorkers()
    {
        IEnumerable<Worker> workers = await repositoryWrapper.normalDbWrapper.worker.GetAll();
        return (ICollection<Worker>)workers;
    }
}