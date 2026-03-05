using Microsoft.EntityFrameworkCore;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
  
        public interface IVehicleService
        {
            Task<VehicleServiceDTO> AddVehicle(VehicleServiceDTO dto);

            Task<List<VehicleServiceDTO>> GetVehicles(int userId);
        Task<VehicleServiceDTO> GetVehicleById(int id);
        

        Task<VehicleServiceDTO> UpdateVehicle(VehicleServiceDTO dto);

            Task<bool> DeleteVehicle(int id);
        Task<List<ExpiryAlertDTO>> GetExpiryAlerts(int userId, int days);
        
    }
    }

