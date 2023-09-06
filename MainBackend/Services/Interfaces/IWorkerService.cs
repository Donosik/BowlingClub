using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IWorkerService
{
    Task<ICollection<Worker>> GetWorkers();
}