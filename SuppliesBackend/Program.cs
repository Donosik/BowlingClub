using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuppliesBackend.Database.Generic.Repositories;
using SuppliesBackend.Database.SuppliesDb.Repositories;
using SuppliesBackend.Database.SuppliesDb.RepositoryWrapper;
using SuppliesBackend.Services.Classes;
using SuppliesBackend.Services.Interfaces;
using SuppliesBackend.Services.Wrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
void ConfigureServices(IServiceCollection services)
{
services.AddScoped<IOrderService, OrderService>();

services.AddScoped<IProductRepository, ProductRepository>();
}

// Wrappers
void ConfigureWrappers(IServiceCollection services)
{
    services.AddScoped<IServiceWrapper, ServiceWrapper>();
    services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
}
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