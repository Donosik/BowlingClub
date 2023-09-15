using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IBarInventoryService
{
    Task<ICollection<BarInventory>> GetBarItems();
    Task<bool> AddBarItem(string name, int quantity, float price);
    Task<bool> DeleteBarItem(int id);
}