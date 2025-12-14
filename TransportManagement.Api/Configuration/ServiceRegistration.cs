using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransportManagement.Application;
using TransportManagement.Application.Common.Behaviors;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Interfaces.Authentication;
using TransportManagement.Application.Mapping;
using TransportManagement.Application.Services;
using TransportManagement.Application.Validations.Vehicles;
using TransportManagement.Infrastructure;
using TransportManagement.Infrastructure.ExternalService;
using TransportManagement.Infrastructure.Persistence;
using TransportManagement.Infrastructure.Repositories;

namespace TransportManagement.Api.Configurations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, IConfiguration configuration)
        {

            // DbContext
            services.AddDbContext<TransportDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Dbconnection")));
            // Identity Injection 
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            services.AddScoped<IGpsTrackingService, WialonGpsAdapter>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpClient<IGpsTrackingService, WialonGpsAdapter>(client =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // AutoMapper
            services.AddAutoMapper(typeof(AssemblyMarker).Assembly);

            // MediatR (Handlers)
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

            // Validation
            services.AddValidatorsFromAssemblyContaining<CreateVehicleDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateVehicleDtoValidator>();

            // Pipeline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            return services;
        }
    }
}
