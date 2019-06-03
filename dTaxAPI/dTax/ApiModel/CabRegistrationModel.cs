using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class CabRegistrationModel
    {
        //public Guid DriverId { get; set; }

        public string LicensePlate { get; set; }
        public int ManufactureYear { get; set; }
        public string VIN { get; set; }


        public Guid CarBrandId { get; set; }
        public Guid CarModelId{ get; set; }
        public Guid CarTypeId { get; set; }
        public Guid CarColorId { get; set; }
    }
}
