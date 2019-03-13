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

        public Guid FileStorageId { get; set; }

        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string ModelType { get; set; }
        public string ModelColor { get; set; }
    }
}
