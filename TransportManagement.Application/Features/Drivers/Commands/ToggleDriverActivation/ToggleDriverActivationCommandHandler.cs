using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Comman.Exceptions;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Drivers.Commands.ToggleDriverActivation
{
    public class ToggleDriverActivationCommandHandler : IRequestHandler<ToggleDriverActivationCommand,Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToggleDriverActivationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ToggleDriverActivationCommand command, CancellationToken cancellationToken)
        {

            var driver = await _unitOfWork.Drivers.GetByIdAsync(command.Id);
            if (driver is null)
                throw new  NotFoundException(nameof(Driver), command.Id);
            driver.ToggleActivation();
            await _unitOfWork.SaveChangesAsync();
            var msg = driver.IsActive
                ? "Driver activated successfully"
                : "Driver deactivated successfully";
            return Result.Success(msg, 200);

        }
    }
}
