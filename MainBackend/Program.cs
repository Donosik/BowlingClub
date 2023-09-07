using System.Text;
using System.Text.Json.Serialization;
using MainBackend.Databases.BowlingDb.Context;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.BowlingDb.RepositoryWrapper;
using MainBackend.Databases.BowlingDw.Context;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Classes;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injecting string connections to database contexts from appsettings.json
builder.Services.AddDbContext<BowlingDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BowlingDatabase")));
builder.Services.AddDbContext<BowlingDw>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BowlingDatawarehouse")));

// Dependency Injection
// Repositories
void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IClientRepository, ClientRepository>();
    services.AddScoped<ILaneRepository, LaneRepository>();
    services.AddScoped<IPersonRepository, PersonRepository>();
    services.AddScoped<IReservationRepository, ReservationRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IWorkerRepository, WorkerRepository>();
    services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
}

// Services
void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IGeneratorService, GeneratorService>();
    services.AddScoped<ILanesService, LaneService>();
    services.AddScoped<IPersonService, PersonService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IWorkerService, WorkerService>();
    services.AddScoped<IWorkScheduleService, WorkScheduleService>();
}

// Wrappers
void ConfigureWrappers(IServiceCollection services)
{
    services.AddScoped<IRepositoryWrapperDb, RepositoryWrapperDb>();
    services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    services.AddScoped<IServiceWrapper, ServiceWrapper>();
}

ConfigureRepositories(builder.Services);
ConfigureServices(builder.Services);
ConfigureWrappers(builder.Services);

//Json Serialization
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();