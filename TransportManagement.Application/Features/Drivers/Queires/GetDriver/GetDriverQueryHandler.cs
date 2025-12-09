using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Exceptions;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Queires.GetDriver
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, Result<DriverDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async  Task<Result<DriverDto>>  Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(request.id);
            if (driver == null)
                throw new NotFoundException(nameof(request), request.id);
            var driverdto = _mapper.Map<DriverDto>(driver);
            return Result< DriverDto> .Success(driverdto,"Driver Retrived Succed");
        }

    }
}
