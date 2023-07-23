using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IUserService userService { get; }

#region Constructors

    public ServiceWrapper(IUserService userService)
    {
        this.userService = userService;
    }

#endregion
}