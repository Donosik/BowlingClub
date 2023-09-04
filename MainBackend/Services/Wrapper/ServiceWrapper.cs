using MainBackend.Services.Interfaces;

namespace MainBackend.Services.Wrapper;

public class ServiceWrapper : IServiceWrapper
{
    public IUserService user { get; }
    public IPersonService person { get; }

#region Constructors

    public ServiceWrapper(IUserService user, IPersonService person)
    {
        this.user = user;
        this.person = person;
    }

#endregion
}