using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Classes;

public class BarInventoryService : IBarInventoryService
{
    private IRepositoryWrapper repositoryWrapper;

    public BarInventoryService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<ICollection<BarInventory>> GetBarItems()
    {
        IEnumerable<BarInventory> barInventories = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        return (ICollection<BarInventory>)barInventories;
    }

    public async Task<bool> AddBarItem(string name, int quantity, float price)
    {
        var barItems = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        foreach (var item in barItems)
        {
            if (item.Name == name)
                return false;
        }

        BarInventory barInventory = new BarInventory();
        barInventory.Name = name;
        barInventory.Price = price;
        barInventory.Quantity = quantity;
        repositoryWrapper.normalDbWrapper.barInventory.Create(barInventory);
        return await repositoryWrapper.normalDbWrapper.Save();
    }

    public async Task<bool> DeleteBarItem(int id)
    {
        if (id <= 0)
            return false;
        BarInventory barInventory = await repositoryWrapper.normalDbWrapper.barInventory.Get(id);
        if (barInventory == null)
            return false;
        await repositoryWrapper.normalDbWrapper.barInventory.Delete(id);
        return await repositoryWrapper.normalDbWrapper.Save();
    }
}