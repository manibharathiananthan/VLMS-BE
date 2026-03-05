using AutoMapper;
using DBContext.cs.Data;
using DBContext.cs.Entity;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.Interface;

namespace Service.Implementation
{
    public class ServiceHistoryService : IServiceHistory
    {
        private readonly ApplicationDBContext applicationDB;
        private readonly IMapper mapper;

        public ServiceHistoryService(ApplicationDBContext applicationDB, IMapper mapper)
        {
            this.applicationDB = applicationDB;
            this.mapper = mapper;
        }

        // ADD SERVICE HISTORY
        public async Task<ServiceHistoryDTO> AddServiceHistory(ServiceHistoryDTO serviceHistoryDTO)
        {
            var service = mapper.Map<ServiceHistory>(serviceHistoryDTO);

            await applicationDB.Services.AddAsync(service);
            await applicationDB.SaveChangesAsync();

            return mapper.Map<ServiceHistoryDTO>(service);
        }

        // DELETE SERVICE HISTORY
        public async Task<bool> DeleteHistory(int id)
        {
            var history = await applicationDB.Services.FindAsync(id);

            if (history == null)
                return false;

            applicationDB.Services.Remove(history);
            await applicationDB.SaveChangesAsync();

            return true;
        }

        // GET ALL SERVICE HISTORY
        public async Task<List<ServiceHistoryDTO>> GetAllServiceHistory()
        {
            var history = await applicationDB.Services
                .Include(s => s.Vehicle)
                .ToListAsync();

            return mapper.Map<List<ServiceHistoryDTO>>(history);
        }

        // GET SERVICE BY USER
        public async Task<List<ServiceHistoryDTO>> GetServiceByUser(int userId)
        {
            var services = await applicationDB.Services
                .Include(s => s.Vehicle)
                .Where(s => s.Vehicle.UserId == userId)
                .ToListAsync();

            return mapper.Map<List<ServiceHistoryDTO>>(services);
        }
    }
}