using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class InventoryService : IInventoryService
{
    private IRepositoryWrapper repositoryWrapper;

    public InventoryService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<int> CheckInventoryItemQuantity(string name)
    {
        var inventory = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        return inventory.GroupBy(x=>x.Name).Where(x=>x.Key == name).Select(x=>x.Count()).FirstOrDefault();
    }

    public async Task<ICollection<(string, int)>> CheckAllInventoryItemQuantities()
    {
        var inventory = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        return inventory.GroupBy(x => x.Name).Select(x => (x.Key, x.Count())).ToList();
    }
    
    public async Task<ICollection<Inventory>> GetInventoryItems()
    {
        IEnumerable<Inventory> barInventories = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        return (ICollection<Inventory>)barInventories;
    }

    public async Task<bool> AddInventoryItem(string name, decimal price)
    {
        Inventory inventory = new Inventory();
        inventory.Name = name;
        inventory.Price = price;
        repositoryWrapper.normalDbWrapper.barInventory.Create(inventory);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> DeleteInventoryItem(int id)
    {
        if (id <= 0)
            return false;
        Inventory inventory = await repositoryWrapper.normalDbWrapper.barInventory.Get(id);
        if (inventory == null)
            return false;
        await repositoryWrapper.normalDbWrapper.barInventory.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}