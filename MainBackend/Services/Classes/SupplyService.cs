using System.Collections;
using System.Net;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.DTO;
using MainBackend.Helpers;
using MainBackend.Services.Interfaces;
using RestSharp;

namespace MainBackend.Services.Classes;

public class SupplyService : GeneralRequestHelper, ISupplyService
{
    private IRepositoryWrapper repositoryWrapper;
    private IInventoryService inventoryService;

    //TODO: Dodać żeby adres był z appsettings
    public SupplyService(IRepositoryWrapper repositoryWrapper, IInventoryService inventoryService) : base(
        "https://localhost:44373")
    {
        this.repositoryWrapper = repositoryWrapper;
        this.inventoryService = inventoryService;
    }

    public async Task<Order> GetOrder(int id)
    {
        try
        {
            var request = CreateRequest(Method.Get, $"Order/GetOrder/{id}");
            var response = await SendRequest(request);
            var apiResponse = CheckResponse<Order>(response);
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
        catch (Exception ex)
        {
            var apiResponse = ResponseUnserialized<Order>();
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
    }

    public async Task<ICollection<Order>> GetUnfullfilledOrders()
    {
        try
        {
            var request = CreateRequest(Method.Get, $"Order/GetUnfullfilledOrders");
            var response = await SendRequest(request);
            var apiResponse = CheckResponse<ICollection<Order>>(response);
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
        catch (Exception ex)
        {
            var apiResponse = ResponseUnserialized<ICollection<Order>>();
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
    }

    public async Task<ICollection<Order>> GetFullfilledOrders()
    {
        try
        {
            var request = CreateRequest(Method.Get, $"Order/GetFullfilledOrders");
            var response = await SendRequest(request);
            var apiResponse = CheckResponse<ICollection<Order>>(response);
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
        catch (Exception ex)
        {
            var apiResponse = ResponseUnserialized<ICollection<Order>>();
            if (apiResponse.IsSuccess)
                return apiResponse.Content;
            return null;
        }
    }

    public async Task<bool> CreateOrder(ICollection<Product> products)
    {
        try
        {
            var request = CreateRequest(Method.Post, $"Order/CreateOrder", products);
            var response = await SendRequest(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> CreateNecessaryOrders()
    {
        if (!await AddFullfilledOrdersToDb())
            return false;
        var realOrders = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        var productCountDictionary = new Dictionary<string, int>();
        foreach (var order in realOrders)
        {
            if (productCountDictionary.ContainsKey(order.Name))
                productCountDictionary[order.Name]++;
            else
                productCountDictionary.Add(order.Name, 1);
        }

        var unfullfilledOrders = await GetUnfullfilledOrders();
        if (unfullfilledOrders != null)
            foreach (var unfullfilledOrder in unfullfilledOrders)
            {
                foreach (var product in unfullfilledOrder.Products)
                {
                    if (productCountDictionary.ContainsKey(product.Name))
                        productCountDictionary[product.Name]++;
                    else
                        productCountDictionary.Add(product.Name, 1);
                }
            }

        var targetInventory = await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        foreach (var target in targetInventory)
        {
            if (productCountDictionary.ContainsKey(target.Name))
            {
                var totalProductsCount = productCountDictionary[target.Name];
                var targetProductsCount = target.Quantity;

                if (totalProductsCount >= targetProductsCount)
                    continue;
                else
                {
                    var productsToAdd = targetProductsCount - totalProductsCount;
                    var productsToOrder = new List<Product>();
                    for (int i = 0; i < productsToAdd; i++)
                    {
                        var product = new Product
                        {
                            Name = target.Name,
                        };
                        productsToOrder.Add(product);
                    }

                    if (!await CreateOrder(productsToOrder))
                    {
                        return false;
                    }
                }
            }
            else
            {
                var productsToOrder = new List<Product>();
                for (int i = 0; i < target.Quantity; i++)
                {
                    var product = new Product
                    {
                        Name = target.Name,
                    };
                    productsToOrder.Add(product);
                }

                if (!await CreateOrder(productsToOrder))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public async Task<bool> AddFullfilledOrdersToDb()
    {
        var fullfilledOrders = await GetFullfilledOrders();
        var targetInventory = await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        if (fullfilledOrders != null)
            foreach (var fullfilledOrder in fullfilledOrders)
            {
                foreach (var product in fullfilledOrder.Products)
                {
                    decimal price = targetInventory.Where(x => x.Name == product.Name).Select(x => x.Price)
                        .FirstOrDefault();
                    if (!await inventoryService.AddInventoryItem(product.Name, price))
                        return false;
                }
            }

        return true;
    }
}