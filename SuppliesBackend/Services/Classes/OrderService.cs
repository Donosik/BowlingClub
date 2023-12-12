using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Matching;
using SuppliesBackend.Database.SuppliesDb.Entities;
using SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;
using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Classes;

public class OrderService : IOrderService
{
    private readonly IRepositoryWrapper repositoryWrapper;
    
    public OrderService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }
    public Task<bool> GetOrderStatus(int orderId)
    {
        throw new NotImplementedException();
    }
    public Task<Order> GetCompletedOrder(int orderId)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> CreateOrder(ICollection<Product> products)
    {
        ICollection<Product> productsInDb = new List<Product>();
        foreach (var product in products)
        {
            var productInDb = await repositoryWrapper.product.GetByName(product.Name);
            if (productInDb != null)
                productsInDb.Add(productInDb);
        }
        ICollection<Product> productsNotInDb= new List<Product>();
        foreach (var product in products)
        {
            bool productIsInDb = false;
            foreach (var productInDb in productsInDb)
            {
                if (product.Name == productInDb.Name)
                    productIsInDb = true;
            }
            if(productIsInDb == false)
                productsNotInDb.Add(product);
        }
        int productsAdded=0;
        if (productsNotInDb.Count > 0)
        {
            foreach (var product in productsNotInDb)
            {
                repositoryWrapper.product.Create(product);
                productsAdded++;
            }

            if (!await repositoryWrapper.Save(productsAdded))
                return false;
        }
        Order order = new Order();
        foreach (var product in productsInDb)
        {
            order.Products.Add(product);
        }
        foreach (var product in productsNotInDb)
        {
            order.Products.Add(product);
        }
        repositoryWrapper.order.Create(order);
        if (await repositoryWrapper.Save())
            return true;
        return false;
    }
    public async Task<bool> FulfillOrder(int orderId)
    {
        Order order= await repositoryWrapper.order.Get(orderId);
        if (order != null)
        {
            order.IsFulfilled = true;
            repositoryWrapper.order.Edit(order);
            if (await repositoryWrapper.Save())
                return true;
        }

        return false;
    }
}