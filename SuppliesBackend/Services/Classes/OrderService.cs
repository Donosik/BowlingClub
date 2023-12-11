using System;
using System.Threading.Tasks;
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
    public async Task<bool> CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }
}