using System.Threading.Tasks;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Services.Interfaces;

public interface IOrderService
{
    Task<bool> GetOrderStatus(int orderId);
    Task<Order> GetCompletedOrder(int orderId);
    Task<bool> CreateOrder(ICollection<Product> products);
    Task<bool> FulfillOrder(int orderId);
}