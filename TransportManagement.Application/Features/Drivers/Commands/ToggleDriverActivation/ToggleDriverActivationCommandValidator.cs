using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Drivers.Commands.ToggleDriverActivation
{
    public class ToggleDriverActivationCommandValidator : AbstractValidator<ToggleDriverActivationCommand>
    {
        public ToggleDriverActivationCommandValidator() {
            RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Driver Id is required");
        }
    }
}
