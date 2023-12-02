using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IRegulationService
{
    Task<ICollection<Regulation>> GetRegulations();
    Task<bool> AddRegulation(Regulation regulation);
    Task<bool> ChangeRegulations(ICollection<Regulation> regulations);
    Task<bool> DeleteRegulation(int id);
}