using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Drivers.Commands.UpdateDriver
{
     public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
    {
        public UpdateDriverCommandValidator()
        {
            RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Driver Id is required");

            RuleFor(x => x.Dto.FullName)
                .NotEmpty().WithMessage("FullName is required")
                .MaximumLength(50).WithMessage("FullName must not exceed 50 characters");

            RuleFor(x => x.Dto.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required")
                .MaximumLength(15).WithMessage("PhoneNumber must not exceed 15 characters")
                .Matches(@"^\+?[0-9]{6,15}$")
                .WithMessage("Phone number format is invalid");

            RuleFor(x => x.Dto.IsActive)
                .NotNull().WithMessage("IsActive must be specified");
        }
    }
}
