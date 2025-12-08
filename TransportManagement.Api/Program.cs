using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TransportManagement.Application;
using TransportManagement.Application.Common.Behaviors;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Mapping;
using TransportManagement.Application.Validations.Vehicles;
using TransportManagement.Infrastructure;
using TransportManagement.Infrastructure.ExternalService;
using TransportManagement.Infrastructure.Persistence;
using TransportManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper
builder.Services.AddAutoMapper(
    cfg => { },                 // åäÇ Action ÝÇÖí
    typeof(AssemblyMarker).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper((typeof(VehicleProfile)));
builder.Services.AddAutoMapper(typeof(DriverProfile));
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Transport API",
        Version = "v1"
    });
});

// DbContext
builder.Services.AddDbContext<TransportDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconnection")));

// Generic Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Specialized repositories
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IGpsTrackingService, WialonGpsAdapter>();
/*builder.Services.AddHttpClient<WialonGpsAdapter>(client =>
{
    client.BaseAddress = new Uri("https://wialon-api.sa");
});*/
builder.Services.AddHttpClient<IGpsTrackingService, WialonGpsAdapter>(client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

});



// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

//Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateVehicleDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateVehicleDtoValidator>();
//pipeline behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(TransportManagement.Application.AssemblyReference).Assembly);
});




var app = builder.Build();

// Middleware
app.UseMiddleware<TransportManagement.Api.Middlewares.ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
