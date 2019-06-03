using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class DriverRegistrationModel
    {
        //public Guid UserId { get; set; }

        public long DrivingLicence { get; set; } 
        public DateTime ExpiryDate { get; set; }

        public DateTime RegistrationDate { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }

        //public long FileStorageId { get; set; }

        ////Cab
        //public string LicensePlate { get; set; }
        //public int ManufactureYear { get; set; }
        //public string VIN { get; set; }

        ////CarModel
        //public string BrandName { get; set; }
        //public string ModelName { get; set; }
        //public string ModelDescription { get; set; }

    }
}
