using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.DTO;

namespace MainBackend.Services.Interfaces;

public interface ITargetInventoryService
{
    Task<ICollection<TargetInventory>> GetTargetInventoryItems();
    Task<ICollection<InventoryStatus>> GetMagazineStatus();
    Task<ICollection<InventoryStatus>> GetMagazineStatus(int usersPerPage, int currentPage);
    Task<bool> AddTargetInventoryItem(TargetInventory targetInventory);
    Task<bool> UpdateTargetInventoryItem(int id,TargetInventory targetInventory);
    Task<bool> DeleteTargetInventoryItem(int id);
}