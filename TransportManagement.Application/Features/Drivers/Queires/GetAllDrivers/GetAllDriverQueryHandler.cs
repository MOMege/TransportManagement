using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Drivers.Queires.GetAllDrivers
{
    public class GetAllDriverQueryHandler : IRequestHandler<GetAllDriverQuery, Result<IEnumerable<DriverDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) { 
        _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task <Result<IEnumerable<DriverDto>>> Handle(GetAllDriverQuery request, CancellationToken cancellationToken)
        {
           var Drivers= await _unitOfWork.Drivers.GetAllAsync();
            var listdto=_mapper.Map <IEnumerable < DriverDto >> (Drivers);
            return Result<IEnumerable<DriverDto>>.Success(listdto, "All Drivers");
                
        }
    }
}
