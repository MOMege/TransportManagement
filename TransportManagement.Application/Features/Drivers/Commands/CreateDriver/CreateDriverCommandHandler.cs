using AutoMapper;
using Azure.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagement.Application.Interfaces;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Application.Features.Drivers.Commands.CreateDriver
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand,Result<Guid>>

    {
    private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDriverCommandHandler(IUnitOfWork unitOfWork,IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateDriverCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Driver>(command.dto);
             await _unitOfWork.Drivers.AddSync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(entity.Id, "Driver is created Correctly");

        }
    }

    
}
