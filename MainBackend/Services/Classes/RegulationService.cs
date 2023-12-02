using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class RegulationService : IRegulationService
{
    private readonly IRepositoryWrapper repositoryWrapper;

    public RegulationService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }
    public async Task<ICollection<Regulation>> GetRegulations()
    {
        ICollection<Regulation> regulations = await repositoryWrapper.normalDbWrapper.regulation.GetAll();
        return regulations;
    }

    public async Task<bool> AddRegulation(Regulation regulation)
    {
        repositoryWrapper.normalDbWrapper.regulation.Create(regulation);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> ChangeRegulations(ICollection<Regulation> regulations)
    {
        var currentRegulations = await GetRegulations();
        int updatedEntities = 0;
        foreach (var regulation in regulations)
        {
            var existingRegulation = currentRegulations.FirstOrDefault(x => x.number == regulation.number);
            if (existingRegulation != null)
            {
                existingRegulation.description = regulation.description;
                repositoryWrapper.normalDbWrapper.regulation.Edit(existingRegulation);
                updatedEntities++;
            }
        }
        return await repositoryWrapper.normalDbWrapper.Save(updatedEntities);
    }

    public async Task<bool> DeleteRegulation(int id)
    {
        await repositoryWrapper.normalDbWrapper.regulation.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

}