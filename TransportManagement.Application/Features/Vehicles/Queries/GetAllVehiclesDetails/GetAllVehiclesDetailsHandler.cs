using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Application.Features.Vehicles.Queries.GetAllVehiclesDetails
{
    public class GetAllVehiclesDetailsHandler : IRequestHandler<GetAllVehicleDetailsQuery,IEnumerable<VehicleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllVehiclesDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
                _mapper = mapper;
        }

        public async Task<IEnumerable<VehicleDto>> Handle(GetAllVehicleDetailsQuery request ,CancellationToken cancellationToken)
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            var listvehicles = _mapper.Map<IEnumerable< VehicleDto>>(vehicles);
            return listvehicles;

        }
    }
}
