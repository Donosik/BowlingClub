using MainBackend.Databases.BowlingDb.Entities;

namespace MainBackend.Services.Interfaces;

public interface IInventoryService
{
    Task<int> CheckInventoryItemQuantity(string name);
    Task<ICollection<(string, int)>> CheckAllInventoryItemQuantities();
    Task<ICollection<Inventory>> GetInventoryItems();
    Task<bool> AddInventoryItem(string name, decimal price,bool isBar);
    Task<bool> DeleteInventoryItem(int id);
}