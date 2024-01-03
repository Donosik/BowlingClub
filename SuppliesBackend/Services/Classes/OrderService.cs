using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Matching;
using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;
using SuppliesBackend.DTO;
using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Classes;

public class OrderService : IOrderService
{
    private readonly IRepositoryWrapper repositoryWrapper;

    public OrderService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<Order> GetOrder(int id)
    {
        return await repositoryWrapper.order.Get(id);
    }

    public async Task<ICollection<Order>> GetUnfullfilledOrders()
    {
        var orders = await repositoryWrapper.order.GetAll();
        orders = orders.Where(o => o.IsFullfilled == false).ToList();
        return orders;
    }

    public async Task<ICollection<Order>> GetFullfilledOrders()
    {
        var orders = await repositoryWrapper.order.GetAll();
        orders = orders.Where(o => o.IsFullfilled == true && o.IsTaken == false).ToList();
        return orders;
    }

    public async Task<bool> CreateOrder(ICollection<ProductDTO> products)
    {
        if (products.Count == 0)
            return false;
        Order order = new Order();
        ICollection<Product> productsToAdd = new List<Product>();
        foreach (var product in products)
        {
            Product p = new Product();
            p.Name = product.Name;
            productsToAdd.Add(p);
        }
        order.Products = productsToAdd;
        repositoryWrapper.order.Create(order);
        if (await repositoryWrapper.Save())
            return true;
        return false;
    }

    public async Task<bool> FullfillOrder(int orderId, int clientId)
    {
        User client = await repositoryWrapper.user.GetUserWithOrders(clientId);
        if (client == null)
            return false;
        Order order = await repositoryWrapper.order.Get(orderId);
        if (order != null)
        {
            order.IsFullfilled = true;
            client.FullfilledOrders.Add(order);
            repositoryWrapper.order.Edit(order);
            if (await repositoryWrapper.Save())
                return true;
        }

        return false;
    }

    public async Task<bool> TakeOrders(ICollection<Order> orders)
    {
        int edited = 0;
        foreach (var order in orders)
        {
            var orderInDb = await repositoryWrapper.order.Get(order.Id);
            if (orderInDb != null)
            {
                orderInDb.IsTaken = true;
                repositoryWrapper.order.Edit(orderInDb);
                edited++;
            }
        }

        if (await repositoryWrapper.Save(edited))
            return true;
        return false;
    }

    public async Task<ICollection<Order>> GetMyOrders(int clientId)
    {
        var user= await repositoryWrapper.user.GetOrdersWithProducts(clientId);
        
        return user.FullfilledOrders;
    }
}