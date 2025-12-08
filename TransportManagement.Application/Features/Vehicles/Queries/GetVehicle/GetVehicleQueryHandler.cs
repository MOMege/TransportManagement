using AutoMapper;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Application.Features.Vehicles.Queries.GetVehicle
{
    public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery,VehicleDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetVehicleQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<VehicleDto> Handle(GetVehicleQuery entity, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(entity.id);
            if (vehicle == null)
                throw new KeyNotFoundException($"Vehicle with Id {entity.id} not found");

            var vehicledto = _mapper.Map<VehicleDto>(vehicle);
            
            return vehicledto;
        }
    }
}
