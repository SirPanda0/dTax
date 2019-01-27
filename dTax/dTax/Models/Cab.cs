using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class Cab
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public int CarModelId { get; set; }
        public CarModel CarModel { get; set; }
        public int ManufactureYear { get; set; }
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public bool Active { get; set; }
    }
}
