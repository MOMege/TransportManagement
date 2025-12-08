using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TransportManagement.Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator:AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Plate number is required")
                .MaximumLength(10).WithMessage("Plate number must not exceed 10 characters");

            RuleFor(x => x.MaxLoadKg)
                .GreaterThan(0).WithMessage("Max Load must be greater than 0");

            RuleFor(x => x.DoorNumber)
                .GreaterThan(0).WithMessage("Door number must be greater than 0");
        }
    }
}
