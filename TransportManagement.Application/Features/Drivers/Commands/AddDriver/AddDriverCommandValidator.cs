using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Drivers.Commands.AddDriver
{
    public class AddDriverCommandValidator :AbstractValidator<AddDriverCommand>
    {
      public AddDriverCommandValidator() { 
        
            RuleFor(x => x.FullName)
                .NotNull().WithMessage("Please Enter Your FullName")
                .NotEmpty().WithMessage("Must Enter Your FullName")
                .MaximumLength(30).WithMessage("Plate number must not exceed 30 characters");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Enter your Phone NUmber")
                .MaximumLength(15).WithMessage("Phone Number must not exceed 15 characters")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format"); 
            

        }
    }
}
