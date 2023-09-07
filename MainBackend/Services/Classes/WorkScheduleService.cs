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

    public async Task<ICollection<WorkSchedule>> GetWorkSchedules()
    {
        IEnumerable<WorkSchedule> workSchedules = await repositoryWrapper.normalDbWrapper.workSchedule.GetAll();
        return (ICollection<WorkSchedule>)workSchedules;
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

    public async Task<bool> DeleteShift(int id)
    {
        if (id <= 0)
            return false;
        WorkSchedule workSchedule = await repositoryWrapper.normalDbWrapper.workSchedule.Get(id);
        if (workSchedule == null)
            return false;
        await repositoryWrapper.normalDbWrapper.workSchedule.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
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