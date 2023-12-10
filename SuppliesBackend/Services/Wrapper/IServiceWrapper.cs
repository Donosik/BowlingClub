using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IOrderService order { get; }
}