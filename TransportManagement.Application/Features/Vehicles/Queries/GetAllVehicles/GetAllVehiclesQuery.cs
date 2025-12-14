
using TransportManagement.Domain.Entites;
using MediatR;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Comman.Pagination;
using Microsoft.AspNetCore.Http;
using TransportManagement.Application.Wrappers;


namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesQuery() : PaginationRequest, IRequest<Result< PagedResult<VehicleListDto>>> 
{
    public bool? IsActive;
    public string? Search;
}

