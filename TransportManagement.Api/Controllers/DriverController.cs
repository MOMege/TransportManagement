using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Features.Drivers.Queires.GetAllDrivers;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : Controller

     
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
       
        public DriverController(IMapper mapper , IMediator mediator) {
        this._mapper = mapper;
        this._mediator = mediator;
    
    }

        [HttpGet("test")]
        public IActionResult TestMapping()
        {
            // وزعنا بيانات تجريبية كأنها جاية من DB
            var driver = new Driver("Mohamed Ashraf", "0500000000");

            // نعمل Mapping للـ DTO
            var dto = _mapper.Map<DriverDto>(driver);

            return Ok(dto);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBYMediatr()
        {
            var result = await _mediator.Send(new GetAllDriverQuery());
            return Ok(result);
        }
    }
}
