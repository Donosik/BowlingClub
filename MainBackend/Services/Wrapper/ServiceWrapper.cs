using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IUserService userService { get; }
    public IPersonService personService { get; }

#region Constructors

    public ServiceWrapper(IUserService userService, IPersonService personService)
    {
        this.userService = userService;
        this.personService = personService;
    }

#endregion
}