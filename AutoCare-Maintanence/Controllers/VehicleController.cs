using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;
using System.Security.Claims;

namespace AutoCare_Maintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // ADD VEHICLE
        [HttpPost]
        public async Task<IActionResult> AddVehicle(VehicleServiceDTO dto)
        {
            var result = await _vehicleService.AddVehicle(dto);
            return Ok(result);
        }

        // GET VEHICLES BY USER
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetVehicles(int userId)
        {
            var vehicles = await _vehicleService.GetVehicles(userId);
            return Ok(vehicles);
        }

        // UPDATE VEHICLE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVehicle(int id, VehicleServiceDTO dto)
        {
            if (id != dto.VehicleId)
                return BadRequest("Vehicle ID mismatch");

            var result = await _vehicleService.UpdateVehicle(dto);

            if (result == null)
                return NotFound("Vehicle not found");

            return Ok(result);
        }

        // DELETE VEHICLE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteVehicle(id);

            if (!result)
                return NotFound("Vehicle not found");

            return Ok("Vehicle deleted successfully");
        }

        // EXPIRY ALERTS - NO AUTH REQUIRED
        [HttpGet("expiry-alert")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExpiryAlert([FromQuery] int userId, [FromQuery] int days = 30)
        {
            if (userId <= 0)
                return BadRequest("Invalid UserId");

            var result = await _vehicleService.GetExpiryAlerts(userId, days);
            return Ok(result);
        }
    }
}