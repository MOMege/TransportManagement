using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Application.Features.Drivers.Queires.GetDriver
{
    public class GetDriverQueryValidator : AbstractValidator<GetDriverQuery>
    {
        public GetDriverQueryValidator() {
            RuleFor(x => x.id)
                     .NotEmpty().WithMessage("Driver Id is required!")
                     .NotEqual(Guid.Empty).WithMessage("Invalid Driver Id!");
        }
    }
}
