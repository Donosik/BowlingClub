using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class RaportController : ControllerBase
{
    private readonly IServiceWrapper serviceWrapper;

    public RaportController(IServiceWrapper serviceWrapper)
    {
        this.serviceWrapper = serviceWrapper;
    }
}