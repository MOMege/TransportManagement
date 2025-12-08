using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Vehicles;
using FluentValidation;

namespace TransportManagement.Application.Validations.Vehicles
{
    public class CreateVehicleDtoValidator:AbstractValidator<CreateVehicleDto>
    {
        public CreateVehicleDtoValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty().WithMessage("Plate number is required")
                .MaximumLength(10).WithMessage("Max length is 10 characters");

            RuleFor(x => x.DoorNumber)
                .GreaterThan(0).WithMessage("Door number must be > 0")
                .LessThanOrEqualTo(1000).WithMessage("Too many doors!");

            RuleFor(x => x.MaxLoadKg)
                .GreaterThan(0).WithMessage("Load must be valid positive value");
        }
    
    }
}
