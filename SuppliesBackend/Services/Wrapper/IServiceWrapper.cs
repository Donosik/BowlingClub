using SuppliesBackend.Database.SuppliesDb.Repositories;
using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IOrderService order { get; }
    IUserService user { get; }
}