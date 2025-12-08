using AutoMapper;
using MediatR;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;


namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;

public class GetAllVehiclesQueryHandler
    : IRequestHandler<GetAllVehiclesQuery, IEnumerable<VehicleListDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllVehiclesQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper= mapper;
    }

    public async Task<IEnumerable<VehicleListDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
        var dtos= _mapper.Map<IEnumerable<VehicleListDto>>(vehicles);

        return dtos;
    }
}
