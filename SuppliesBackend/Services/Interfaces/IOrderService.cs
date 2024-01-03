using System.Threading.Tasks;
using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.DTO;

namespace SuppliesBackend.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrder(int id);
    Task<ICollection<Order>> GetUnfullfilledOrders();
    Task<ICollection<Order>> GetFullfilledOrders();
    Task<bool> CreateOrder(ICollection<ProductDTO> products);
    Task<bool> FullfillOrder(int orderId,int clientId);
    Task<bool> TakeOrders(ICollection<Order> orders);
    Task<ICollection<Order>> GetMyOrders(int clientId);
}