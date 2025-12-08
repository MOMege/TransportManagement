using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransportManagement.Application.DTOs.Vehicles;
using TransportManagement.Application.ExternalServices.Query;
using TransportManagement.Application.Features.Vehicles.Commands.UpdateVehicle;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehicles;
using TransportManagement.Application.Features.Vehicles.Queries.GetAllVehiclesDetails;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;

namespace TransportManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public VehiclesController(IUnitOfWork unitOfWork, IMapper mapper,IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        //External resource
        [HttpGet("location/{plateNumber}")]
        public async Task<IActionResult> GetLocation(string plateNumber)
        {
            var result = await _mediator.Send(new GetVehicleLocationQuery(plateNumber));
            return Ok(result);
        }

        //worked
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBYMediatr()
        {
            var result = await _mediator.Send(new GetAllVehiclesQuery());
            return Ok(result);
        }
        /*
        //worked
        // GET: api/Vehicles
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _unitOfWork.Vehicles.GetAllAsync();
            var result = _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
            return Ok(result);
        }

        */
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVehiclesQuery());
            return Ok(result);
        }

        [HttpGet ("Details")]
        public async Task<IActionResult> GetAllVehicleDetails()
        {
            var result = await _mediator.Send(new GetAllVehicleDetailsQuery());
            return Ok(result);
        }
        //worked
        // GET: api/Vehicles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle is null)
                return NotFound();

            var result = _mapper.Map<VehicleDto>(vehicle);
            return Ok(result);
        }

        //worked
        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleDto dto)
        {
            var entity = _mapper.Map<Vehicle>(dto);

            await _unitOfWork.Vehicles.AddSync(entity);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<VehicleDto>(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, result);
        }
        /*
        // PUT: api/Vehicles/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateData(Guid id, UpdateVehicleDto dto)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle is null)
                return NotFound();

            _mapper.Map(dto, vehicle);

            _unitOfWork.Vehicles.UpdateSync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<VehicleDto>(vehicle);
            return Ok(result);
        }

        */
        //worked
        // DELETE: api/Vehicles/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(id);
            if (vehicle is null)
                return NotFound();

            _unitOfWork.Vehicles.DeleteSync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            return Ok("Vehicle deleted");
        }

        //worked
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateVehicleCommand command)
        {
            // نلغي الـ Id اللي جاي من الـ Body و نثبت اللي من الـ Route فقط
            command = command with { Id = id };

            var result = await _mediator.Send(command);

            return result ? NoContent() : NotFound();
        }

    }
}
