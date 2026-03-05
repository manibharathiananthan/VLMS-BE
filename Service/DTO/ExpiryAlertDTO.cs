using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class ExpiryAlertDTO
    {
    
            public string DocumentType { get; set; } = null!;
            public string? VehicleNumber { get; set; }
            public DateTime ExpiryDate { get; set; }
        
    }
}

