using MainBackend.DTO;
using RestSharp;

namespace MainBackend.Services.Interfaces;

public interface ISupplyService
{
    Task<Order> GetOrder(int id);
    Task<ICollection<Order>> GetUnfullfilledOrders();
    Task<ICollection<Order>> GetFullfilledOrders();
    Task<bool> CreateOrder(ICollection<Product> products);
    Task<bool> CreateNecessaryOrders();
    Task<bool> AddFullfilledOrdersToDb();
}