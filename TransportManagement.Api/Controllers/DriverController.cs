using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Features.Drivers.Commands.DeleteDriver;
using TransportManagement.Application.Features.Drivers.Commands.ToggleDriverActivation;
using TransportManagement.Application.Features.Drivers.Commands.UpdateDriver;
using TransportManagement.Application.Features.Drivers.Queires.GetAllDrivers;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;
using TransportManagement.Application.Wrappers;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : Controller

     
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<DriverController> _logger;

        public DriverController(IMapper mapper , IMediator mediator, ILogger<DriverController> logger)
        {
            this._mapper = mapper;
            this._mediator = mediator;
            _logger = logger;
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
        // GET api/drivers/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // 1️⃣ نبعت Query للـ MediatR
            var result = await _mediator.Send(new Application.Features.Drivers.Queires.GetDriver.GetDriverQuery(id));

            // 2️⃣ لو فشل نرجع Status Code مناسب
            if (!result.Succeeded)
            {
                if (result.StatusCode == 404)
                    return NotFound(result);   // body فيه message + succeeded + statuscode

                // أي خطأ تاني
                var statusCode = result.StatusCode == 0 ? 500 : result.StatusCode;
                return StatusCode(statusCode, result);
            }

            // 3️⃣ لو نجح نرجع Ok ومعاه الـ Result<Data>
            return Ok(result); // { succeeded=true, message="", data={...driver...} }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBYMediatr()
        {
            var result = await _mediator.Send(new GetAllDriverQuery());
            _logger.LogInformation("🔥 File Logger Test - Logging from Controller!");
            return Ok(result);
        }
        /// <summary>
        /// Update Driver info by Id
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Result>> Update(Guid id, [FromBody] UpdateDriverDto dto)
        {
            var command = new UpdateDriverCommand(id, dto);

            var result = await _mediator.Send(command);

            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Result>> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteDriverCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id:guid}/toggle")]
        public async Task<ActionResult<Result>> ToggleActivation(Guid id)
        {
            var result = await _mediator.Send(new ToggleDriverActivationCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
