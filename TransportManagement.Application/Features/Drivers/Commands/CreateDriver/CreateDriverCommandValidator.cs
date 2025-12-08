using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.DTOs.Driveres;

namespace TransportManagement.Application.Features.Drivers.Commands.CreateDriver
{
    public class CreateDriverCommandValidator :AbstractValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator() {


            RuleFor(x=> x.dto.FullName)
                .NotNull().WithMessage("Please Enter Your FullName")
                .NotEmpty().WithMessage("Must Enter Your FullName")
                .MaximumLength(30).WithMessage("Plate number must not exceed 30 characters");
            RuleFor(x => x.dto.PhoneNumber)
                .NotEmpty().WithMessage("Enter your Phone NUmber")
                .MaximumLength(15).WithMessage("Phone Number must not exceed 15 characters")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format");
        }
    }
}
