using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Application.Features.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(command.id);
            if (vehicle is null)
            throw new KeyNotFoundException($"Vehicle with Id {command.id} not found");
            _unitOfWork.Vehicles.DeleteSync(vehicle);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;

        }

    }
}
