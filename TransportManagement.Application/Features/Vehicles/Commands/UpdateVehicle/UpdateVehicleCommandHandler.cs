using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(
        UpdateVehicleCommand request,
        CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.Id);
            if (vehicle == null) return false;

            vehicle.UpdateDetails(
                request.DoorNumber,
                request.Type,
                request.MaxLoadKg

            );

            await _unitOfWork.SaveChangesAsync();
            return true;


        }
    }
}
