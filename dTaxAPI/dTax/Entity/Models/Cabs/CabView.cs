using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Cabs
{
    public class CabView
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string CarType { get; set; }
        public int ManufactureYear { get; set; }
        public Guid DriverId { get; set; }
    }
}
