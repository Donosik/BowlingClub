using System.Threading.Tasks;
using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Services.Interfaces;

public interface IOrderService
{
    Task<Order> GetOrder(int id);
    Task<ICollection<Order>> GetUnfullfilledOrders();
    Task<ICollection<Order>> GetFullfilledOrders();
    Task<bool> CreateOrder(ICollection<Product> products);
    Task<bool> FullfillOrder(int orderId);
}