using MainBackend.Databases.BowlingDb.Context;
using MainBackend.Databases.BowlingDb.Entities;
using MainBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MainBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private BowlingDb _context;
    private IUserService userService;
    
    public WeatherForecastController(ILogger<WeatherForecastController> logger,BowlingDb context,IUserService userService)
    {
        _context = context;
        _logger = logger;
        this.userService = userService;
    }

    [HttpGet("Get")]
    public IActionResult Get1()
    {
        User user = new User();
        user.Login = 1;
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok();
    }
    
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
    
    [HttpGet("{id}")]
    public IActionResult CreateUser(int id)
    {
        userService.Create(id);
        return Ok();
    }
}