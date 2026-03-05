using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interface;

namespace VehicleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly IServiceHistory serviceHistory;

        public ServiceHistoryController(IServiceHistory serviceHistory)
        {
            this.serviceHistory = serviceHistory;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await serviceHistory.GetAllServiceHistory();
            return Ok(data);
        }

        // GET BY USER
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var data = await serviceHistory.GetServiceByUser(userId);
            return Ok(data);
        }

        // ADD SERVICE
        [HttpPost]
        public async Task<IActionResult> Add(ServiceHistoryDTO dto)
        {
            var result = await serviceHistory.AddServiceHistory(dto);
            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await serviceHistory.DeleteHistory(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}