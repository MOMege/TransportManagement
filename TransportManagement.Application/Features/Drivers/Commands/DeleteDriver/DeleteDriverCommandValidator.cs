using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Drivers.Commands.DeleteDriver
{
    public class DeleteDriverCommandValidator : AbstractValidator<DeleteDriverCommand>
    {
        public DeleteDriverCommandValidator() {

            RuleFor(x => x.Id)
                    .NotEmpty().WithMessage(" ID Required ");
        }
    }
}
