using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IUserService userService { get; }
    IPersonService personService { get; }
}