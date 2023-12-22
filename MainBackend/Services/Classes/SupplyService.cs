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

    //TODO: Dodać żeby adres był z appsettings
    public SupplyService(IRepositoryWrapper repositoryWrapper) : base("https://localhost:44373/Supply")
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<Order> GetOrder(int id)
    {
        try
        {
            var request = CreateRequest(Method.Get, $"GetOrder/{id}");
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
            var request = CreateRequest(Method.Get, $"GetUnfullfilledOrders");
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
            var request = CreateRequest(Method.Get, $"GetFullfilledOrders");
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
            var request = CreateRequest(Method.Get, $"CreateOrder", products);
            var response = await SendRequest(request);
            return CheckResponse<bool>(response).IsSuccess;
        }
        catch (Exception ex)
        {
            return ResponseUnserialized<bool>().IsSuccess;
        }
    }

    public async Task<bool> CreateNecessaryOrders()
    {
        var realOrders = await repositoryWrapper.normalDbWrapper.barInventory.GetAll();
        var fullfilledOrders = await GetFullfilledOrders();
        var targetInventory = await repositoryWrapper.normalDbWrapper.targetInventory.GetAll();
        var productCountDictionary = new Dictionary<string, int>();
        foreach (var fullfilledOrder in fullfilledOrders)
        {
            foreach (var product in fullfilledOrder.Products)
            {
                if (productCountDictionary.ContainsKey(product.Name))
                    productCountDictionary[product.Name]++;
                else
                    productCountDictionary.Add(product.Name, 1);
            }
        }

        foreach (var order in realOrders)
        {
            if (productCountDictionary.ContainsKey(order.Name))
                productCountDictionary[order.Name]++;
            else
                productCountDictionary.Add(order.Name, 1);
        }
        foreach (var target in targetInventory)
        {
            if (productCountDictionary.ContainsKey(target.Name))
            {
                var totalProductsCount = productCountDictionary[target.Name];
                var targetProductsCount = target.Quantity;

                if (totalProductsCount > targetProductsCount)
                {
                    // Tutaj dodaj kod do obsługi tworzenia nowych zamówień lub zaktualizowania stanu faktycznego
                    // w zależności od logiki biznesowej
                    // ...

                    // Zwróć true, jeśli utworzono nowe zamówienia lub zaktualizowano stan faktyczny
                    return true;
                }
            }
        }
    }
}