using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class CabRegistration
    {
        public Guid DriverId { get; set; }

        public string LicensePlate { get; set; }
        public int ManufactureYear { get; set; }
        public string VIN { get; set; }


        public long BrandNameCode { get; set; }
        public long ModelNameCode { get; set; }
        public long ModelTypeCode { get; set; }
        public long ModelColorCode { get; set; }
    }
}
