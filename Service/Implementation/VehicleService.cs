using AutoMapper;
using DBContext.cs.Data;
using DBContext.cs.Entity;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Service.Implementation
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public VehicleService(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VehicleServiceDTO> AddVehicle(VehicleServiceDTO dto)
        {
            var vehicle = _mapper.Map<Vehicle>(dto);

            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleServiceDTO>(vehicle);
        }
        public async Task<VehicleServiceDTO> GetVehicleById(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            return _mapper.Map<VehicleServiceDTO>(vehicle);
        }

        public async Task<List<VehicleServiceDTO>> GetVehicles(int userId)
        {
            var vehicles = await _context.Vehicles
                                         .Where(v => v.UserId == userId)
                                         .ToListAsync();

            return _mapper.Map<List<VehicleServiceDTO>>(vehicles);
        }

        public async Task<VehicleServiceDTO> UpdateVehicle(VehicleServiceDTO dto)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleId == dto.VehicleId);

            if (vehicle == null)
                return null;

            
            vehicle.VehicleNumber = dto.VehicleNumber;
            vehicle.Type = dto.Type;
            vehicle.Model = dto.Model;
            vehicle.Brand = dto.Brand;
            vehicle.RCExpiryDate = dto.RCExpiryDate;
            vehicle.InsuranceExpiryDate = dto.InsuranceExpiryDate;
            vehicle.PollutionExpiryDate = dto.PollutionExpiryDate;
            vehicle.BatteryWarrantyExpiryDate = dto.BatteryWarrantyExpiryDate;

            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleServiceDTO>(vehicle);
        }


        public async Task<bool> DeleteVehicle(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<ExpiryAlertDTO>> GetExpiryAlerts(int userId, int days)
        {
            var today = DateTime.UtcNow.Date;
            var futureDate = today.AddDays(days);

            var alerts = new List<ExpiryAlertDTO>();

            var vehicles = await _context.Vehicles
                .Where(v => userId == 0 || v.UserId == userId) // 0 = all users
                .ToListAsync();

            foreach (var v in vehicles)
            {
                if (v.RCExpiryDate <= futureDate)
                    alerts.Add(new ExpiryAlertDTO
                    {
                        DocumentType = "RC",
                        VehicleNumber = v.VehicleNumber,
                        ExpiryDate = v.RCExpiryDate
                    });

                if (v.InsuranceExpiryDate <= futureDate)
                    alerts.Add(new ExpiryAlertDTO
                    {
                        DocumentType = "Insurance",
                        VehicleNumber = v.VehicleNumber,
                        ExpiryDate = v.InsuranceExpiryDate
                    });

                if (v.PollutionExpiryDate <= futureDate)
                    alerts.Add(new ExpiryAlertDTO
                    {
                        DocumentType = "Pollution",
                        VehicleNumber = v.VehicleNumber,
                        ExpiryDate = v.PollutionExpiryDate
                    });

                if (v.BatteryWarrantyExpiryDate <= futureDate)
                    alerts.Add(new ExpiryAlertDTO
                    {
                        DocumentType = "BatteryWarranty",
                        VehicleNumber = v.VehicleNumber,
                        ExpiryDate = v.BatteryWarrantyExpiryDate
                    });
            }

            return alerts.OrderBy(a => a.ExpiryDate).ToList();
        }



    }
}
