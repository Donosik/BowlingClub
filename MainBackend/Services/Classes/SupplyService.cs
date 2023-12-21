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
        var fullfilledOrders = await GetFullfilledOrders();
        
    }
}