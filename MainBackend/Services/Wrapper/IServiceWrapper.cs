using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public interface IServiceWrapper
{
    IUserService user { get; }
    IPersonService person { get; }
}