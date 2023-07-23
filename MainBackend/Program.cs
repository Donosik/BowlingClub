using System.Text.Json.Serialization;
using MainBackend.Databases.BowlingDb.Context;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.BowlingDb.RepositoryWrapper;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Classes;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injecting string connections to database contexts from appsettings.json
builder.Services.AddDbContext<BowlingDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BowlingDb")));

// Dependency Injection
void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IUserRepository, UserRepository>();
}

void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IUserService, UserService>();
}

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