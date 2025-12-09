using AutoMapper;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Exceptions;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Commands.UpdateDriver
{
    public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateDriverCommand command, CancellationToken cancellationToken)
        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(command.Id);
            if (driver == null) 
                throw new NotFoundException(nameof(driver), command.Id);
            driver.Update(
                 command.Dto.FullName,
                 command.Dto.PhoneNumber,
                 command.Dto.IsActive
              );
            await _unitOfWork.SaveChangesAsync();
            return Result.Success("Driver updated successfully", 200);
        }
    }
}
