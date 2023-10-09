using System.Text;
using System.Text.Json.Serialization;
using MainBackend.Databases.BowlingDb.Context;
using MainBackend.Databases.BowlingDb.Repositories.Classes;
using MainBackend.Databases.BowlingDb.Repositories.Interfaces;
using MainBackend.Databases.BowlingDb.RepositoryWrapper;
using MainBackend.Databases.BowlingDw.Context;
using MainBackend.Databases.BowlingDw.Repositories.Classes;
using MainBackend.Databases.BowlingDw.Repositories.Interfaces;
using MainBackend.Databases.BowlingDw.RepositoryWrapper;
using MainBackend.Databases.Generic.Repositories;
using MainBackend.Services.Classes;
using MainBackend.Services.Interfaces;
using MainBackend.Services.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Cors configuration
builder.Services.AddCors();

// Injecting string connections to database contexts from appsettings.json
builder.Services.AddDbContext<BowlingDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BowlingDatabase")));
builder.Services.AddDbContext<BowlingDw>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BowlingDatawarehouse")));

// Dependency Injection
// Repositories for BowlingDb
void ConfigureRepositoriesDb(IServiceCollection services)
{
    services.AddScoped<IBarInventoryRepository, BarInventoryRepository>();
    services.AddScoped<IClientRepository, ClientRepository>();
    services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    services.AddScoped<ILaneRepository, LaneRepository>();
    services.AddScoped<IPersonRepository, PersonRepository>();
    services.AddScoped<IReservationRepository, ReservationRepository>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IWorkerRepository, WorkerRepository>();
    services.AddScoped<IWorkScheduleRepository, WorkScheduleRepository>();
}

// Repositories for BowlingDw
void ConfigureRepositoriesDw(IServiceCollection services)
{
    services.AddScoped<IFactInvoiceRepository, FactInvoiceRepository>();
    services.AddScoped<IFactReservationRepository, FactReservationRepository>();
    services.AddScoped<IFactWorkScheduleRepository, FactWorkScheduleRepository>();
}

// Services
void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IBarInventoryService, BarInventoryService>();
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<IGeneratorService, GeneratorService>();
    services.AddScoped<IInvoiceService, InvoiceService>();
    services.AddScoped<ILanesService, LaneService>();
    services.AddScoped<IPersonService, PersonService>();
    services.AddScoped<IRaportService, RaportService>();
    services.AddScoped<IReservationService, ReservationService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IWorkerService, WorkerService>();
    services.AddScoped<IWorkScheduleService, WorkScheduleService>();
}

// Wrappers
void ConfigureWrappers(IServiceCollection services)
{
    services.AddScoped<IRepositoryWrapperDb, RepositoryWrapperDb>();
    services.AddScoped<IRepositoryWrapperDw, RepositoryWrapperDw>();
    services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    services.AddScoped<IServiceWrapper, ServiceWrapper>();
}

ConfigureRepositoriesDb(builder.Services);
ConfigureRepositoriesDw(builder.Services);
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
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",policy=>policy.RequireRole("Admin"));
    options.AddPolicy("Worker",policy=>policy.RequireRole("Worker"));
    options.AddPolicy("User",policy=>policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors configuration
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();