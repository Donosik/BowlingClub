using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class DataService : IDataService
{
    private readonly IRepositoryWrapper repositoryWrapper;

    public DataService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<OpenHour>> GetOpenHours()
    {
        ICollection<OpenHour> openHours = await repositoryWrapper.normalDbWrapper.openHour.GetAll();
        if (openHours.Count == 7)
            openHours = await CheckDuplicates(openHours);
        if (openHours.Count == 7)
            return openHours;
        throw new Exception("Open Hours count in database is " + openHours.Count);
    }

    public async Task<bool> CreateDefaultOpenHours()
    {
        await DeleteAllOpenHours();
        var defaultOpenHours = new List<OpenHour>();

        for (int i = 1; i <= 7; i++)
        {
            var openHour = new OpenHour
            {
                dayOfWeek = (DayOfWeek)(i % 7) ,
                startTime = new TimeSpan(10, 0, 0),
                endTime = new TimeSpan(20, 0, 0)
            };

            defaultOpenHours.Add(openHour);
        }

        foreach (var openHour in defaultOpenHours)
        {
            repositoryWrapper.normalDbWrapper.openHour.Create(openHour);
        }

        return await repositoryWrapper.normalDbWrapper.Save(defaultOpenHours.Count);
    }

    public async Task<bool> ChangeOpenHours(ICollection<OpenHour> updatedOpenHours)
    {
        var currentOpenHours = await GetOpenHours();
        int updatedEntities = 0;
        foreach (var updatedOpenHour in updatedOpenHours)
        {
            var existingOpenHour = currentOpenHours.FirstOrDefault(o => o.dayOfWeek == updatedOpenHour.dayOfWeek);

            if (existingOpenHour != null)
            {
                existingOpenHour.startTime = updatedOpenHour.startTime;
                existingOpenHour.endTime = updatedOpenHour.endTime;

                repositoryWrapper.normalDbWrapper.openHour.Edit(existingOpenHour);
                updatedEntities++;
            }
        }

        return await repositoryWrapper.normalDbWrapper.Save(updatedEntities);
    }

    public async Task<bool> DeleteAllOpenHours()
    {
        var allOpenHours = await GetOpenHours();
        foreach (var openHour in allOpenHours)
        {
            repositoryWrapper.normalDbWrapper.openHour.Delete(openHour);
        }
        
        return await repositoryWrapper.normalDbWrapper.Save(allOpenHours.Count);
    }

    private async Task<ICollection<OpenHour>> CheckDuplicates(ICollection<OpenHour> openHours)
    {
        var uniqueDays = new HashSet<DayOfWeek>();
        var uniqueOpenHours = new List<OpenHour>();

        foreach (var openHour in openHours)
        {
            if (uniqueDays.Add(openHour.dayOfWeek))
            {
                uniqueOpenHours.Add(openHour);
            }
        }

        return uniqueOpenHours;
    }
}