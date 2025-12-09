using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Exceptions;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;

namespace TransportManagement.Application.Features.Drivers.Commands.DeleteDriver
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand,Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDriverCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeleteDriverCommand command, CancellationToken cancellationToken)

        {
            var driver = await _unitOfWork.Drivers.GetByIdAsync(command.Id);
                if (driver is null)
                    return Result.Failure($"Driver with id {command.Id} not found", 404);
           // hard delete
           /*
            _unitOfWork.Drivers.DeleteSync(driver);
           */
           //soft delete
            driver.Deactivate();
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Driver deactivated successfully", 200);

        }
    }
}
