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
        
        //Cab
        public string LicensePlate { get; set; }
        public int ManufactureYear { get; set; } 

        //CarModel
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }

    }
}
