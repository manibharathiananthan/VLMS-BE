using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
   public class ServiceHistoryDTO
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceCenter { get; set; }
        public decimal Cost { get; set; }
        public string Notes { get; set; }
    }
}
