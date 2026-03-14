using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class VehicleServiceDTO
    {
        public int VehicleId { get; set; }
        public string? VehicleNumber { get; set; }
        public string Type { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public DateTime RCExpiryDate { get; set; }
        public DateTime InsuranceExpiryDate { get; set; }
        public DateTime PollutionExpiryDate { get; set; }
        public DateTime BatteryWarrantyExpiryDate { get; set; }

        public int UserId { get; set; }

    }
}
