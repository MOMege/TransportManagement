using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator:AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("enter number between 1 to 4");
               
            RuleFor(x => x.DoorNumber)
                .GreaterThan(0).WithMessage("Door number must be > 0")
                .LessThanOrEqualTo(1000).WithMessage("Too many doors!");

            RuleFor(x => x.MaxLoadKg)
                .GreaterThan(0).WithMessage("Load must be valid positive value");
        }
    
}
}
