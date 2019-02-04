using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class DriverRegistration
    {
        public int UserId { get; set; }

        public int DrivingLicence { get; set; } 
        public DateTime ExpiryDate { get; set; } 
    }
}
