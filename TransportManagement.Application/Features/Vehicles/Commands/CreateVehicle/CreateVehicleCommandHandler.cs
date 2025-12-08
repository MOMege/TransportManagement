using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Features.Vehicles.Commands.AddVehicle;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateVehicleCommand command, CancellationToken cancellationToke)
        {
            Vehicle vehicle = new Vehicle(command.PlateNumber, command.Type, command.DoorNumber, command.MaxLoadKg);
            await _unitOfWork.Vehicles.AddSync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return vehicle.Id;

        }
    }
}
