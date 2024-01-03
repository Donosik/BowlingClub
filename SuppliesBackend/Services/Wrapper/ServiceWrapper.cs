using SuppliesBackend.Services.Interfaces;

namespace SuppliesBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IOrderService order { get; }
    public IUserService user { get; }
    public ServiceWrapper(IOrderService order,IUserService user)
    {
        this.order = order;
        this.user = user;
    }
}