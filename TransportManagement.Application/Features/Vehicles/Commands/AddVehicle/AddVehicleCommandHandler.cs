using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Vehicles.Commands.AddVehicle
{
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand,Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = new Vehicle(request.PlateNumber, request.Type, request.DoorNumber, request.MaxLoadKg);
            await _unitOfWork.Vehicles.AddSync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return vehicle.Id;
        }
    }
}
