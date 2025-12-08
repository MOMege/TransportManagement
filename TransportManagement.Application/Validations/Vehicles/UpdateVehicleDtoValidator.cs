using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TransportManagement.Application.DTOs.Vehicles;

namespace TransportManagement.Application.Validations.Vehicles
{
    public class UpdateVehicleDtoValidator : AbstractValidator<UpdateVehicleDto>
    {

        public UpdateVehicleDtoValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(x => x.MaxLoadKg)
                .GreaterThan(0);
        }
    }
}
