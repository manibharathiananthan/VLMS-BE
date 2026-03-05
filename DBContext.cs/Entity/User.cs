using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBContext.cs.Entity
{
    public class User
    {
        public int UserId { get; set; }              
        public string UserName { get; set; } = null!;
        public string Phone { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; }=null!;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public List<Vehicle> Vehicles { get; set; }= new List<Vehicle>();
        
    }
}
