using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class TargetInventoryService : ITargetInventoryService
{
    private IRepositoryWrapper repositoryWrapper;
    private IInventoryService inventoryService;

    public TargetInventoryService(IRepositoryWrapper repositoryWrapper, IInventoryService inventoryService)
    {
        this.repositoryWrapper = repositoryWrapper;
        this.inventoryService = inventoryService;
    }
    public async Task<TargetInventory> GetTargetInventoryItem(string name)
    {
        var targetInventory = await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        return targetInventory.FirstOrDefault(x => x.Name == name);
    }

    public async Task<ICollection<TargetInventory>> GetTargetInventoryItems()
    {
        return await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
    }

    public async Task<ICollection<InventoryStatus>> GetMagazineStatus()
    {
        ICollection<TargetInventory> targetInventories =
            await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        var itemQuantities = await inventoryService.CheckAllInventoryItemQuantities();
        var inventoryStatuses = new List<InventoryStatus>();
        foreach (var itemQuantity in targetInventories)
        {
            var inventoryStatus = new InventoryStatus();
            inventoryStatus.Id = itemQuantity.Id;
            inventoryStatus.Name = itemQuantity.Name;
            inventoryStatus.TargetQuantity = itemQuantity.Quantity;
            inventoryStatus.CurrentQuantity = itemQuantities.Where(x => x.Item1 == itemQuantity.Name)
                .Select(x => x.Item2).FirstOrDefault();
            inventoryStatus.Price = itemQuantity.Price;
            inventoryStatuses.Add(inventoryStatus);
        }

        return inventoryStatuses;
    }

    public async Task<bool> AddTargetInventoryItem(TargetInventory targetInventory)
    { var inventory = await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        if (inventory.Any(x => x.Name == targetInventory.Name))
            return false;
        repositoryWrapper.normalDbWrapper.targetInventory.Create(targetInventory);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> UpdateTargetInventoryItem(int id, TargetInventory targetInventory)
    {
        var targetInventoryFromDb = await repositoryWrapper.normalDbWrapper.targetInventory.Get(id);
        if (targetInventoryFromDb == null)
            return false;
        targetInventoryFromDb.Name = targetInventory.Name;
        targetInventoryFromDb.Price = targetInventory.Price;
        targetInventoryFromDb.Quantity = targetInventory.Quantity;
        repositoryWrapper.normalDbWrapper.targetInventory.Edit(targetInventoryFromDb);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> DeleteTargetInventoryItem(int id)
    {
        if (id <= 0)
            return false;
        TargetInventory targetInventory = await repositoryWrapper.normalDbWrapper.targetInventory.Get(id);
        if (targetInventory == null)
            return false;
        await repositoryWrapper.normalDbWrapper.targetInventory.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}