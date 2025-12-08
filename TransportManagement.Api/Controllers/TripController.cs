using Microsoft.AspNetCore.Mvc;
using TransportManagement.Application.Interfaces;

namespace TransportManagement.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TripController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public TripController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("trips-by-vehicle/{vehicleId}")]
        public async Task<IActionResult> GetTripsByVehicle(Guid vehicleId)
        {
            var result = await _unitOfWork.Trips.GetTripsByVehicleIdAsync(vehicleId);
            return Ok(result);
        }

        [HttpGet("trips-today")]
        public async Task<IActionResult> GetTodayTrips()
        {
            var result = await _unitOfWork.Trips.GetTodayTripsAsync();
            return Ok(result);
        }
    }
}
