using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext.cs.Entity
{
    public class Vehicle
    {
        public int VehicleId { get; set; }                
        public string VehicleNumber { get; set; } = null!;
        public string Type { get; set; } = null!;    
        public string Model { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public DateTime RCExpiryDate { get; set; }
        public DateTime InsuranceExpiryDate { get; set; }
        public DateTime PollutionExpiryDate { get; set; }
        public DateTime BatteryWarrantyExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
