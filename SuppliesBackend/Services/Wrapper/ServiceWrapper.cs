using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IOrderService order { get; }
    public ServiceWrapper(IOrderService order)
    {
        this.order = order;
    }
}