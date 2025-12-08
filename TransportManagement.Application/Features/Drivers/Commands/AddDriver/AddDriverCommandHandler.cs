using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Domain.Entites;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;


namespace TransportManagement.Application.Features.Drivers.Commands.AddDriver
{
    public class AddDriverCommandHandler : IRequestHandler<AddDriverCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddDriverCommandHandler(IUnitOfWork unitOfWork)
        { _unitOfWork = unitOfWork; }

        public async Task<Result<Guid>> Handle(AddDriverCommand command, CancellationToken cancellationToken)
        {
            var driver = new Driver(command.FullName, command.PhoneNumber);
            await _unitOfWork.Drivers.AddSync(driver);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(driver.Id,
                "Driver created successfully"
            );

        }
    }
}
