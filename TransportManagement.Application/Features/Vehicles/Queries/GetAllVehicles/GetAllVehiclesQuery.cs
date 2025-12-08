
using TransportManagement.Domain.Entites;
using MediatR;
using TransportManagement.Application.DTOs.Vehicles;


namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;

    public record GetAllVehiclesQuery() : IRequest<IEnumerable<VehicleListDto>>;

