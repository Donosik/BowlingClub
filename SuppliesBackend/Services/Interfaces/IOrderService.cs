using SuppliesBackend.Database.SuppliesDb.Entities;

namespace SuppliesBackend.Services.Interfaces;

public interface IOrderService
{
    Task<bool> CreateOrder(Order order);
}