using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface IDataService
{
    Task<ICollection<OpenHour>> GetOpenHours();
    Task<bool> CreateDefaultOpenHours();
    Task<bool> ChangeOpenHours(ICollection<OpenHour> openHours);
}