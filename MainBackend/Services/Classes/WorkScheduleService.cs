using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class WorkScheduleService : IWorkScheduleService
{
    private IRepositoryWrapper repositoryWrapper;

    public WorkScheduleService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<bool> AddShift(Worker worker, DateTime start, DateTime end)
    {
        if (IsShiftOverlap(worker, start, end))
        {
            return false;
        }
        
        WorkSchedule shift = new WorkSchedule();
        shift.WorkStart = start;
        shift.WorkEnd = end;
        worker.WorkSchedules.Add(shift);
        repositoryWrapper.normalDbWrapper.workSchedule.Create(shift);
        repositoryWrapper.normalDbWrapper.worker.Edit(worker);
        return await repositoryWrapper.normalDbWrapper.Save(2);
    }

    private bool IsShiftOverlap(Worker worker, DateTime start, DateTime end)
    {
        foreach (var existingShift in worker.WorkSchedules)
        {
            if (start < existingShift.WorkEnd && end > existingShift.WorkStart)
            {
                return true;
            }
        }

        return false;
    }
}