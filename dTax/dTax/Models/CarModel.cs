using dTax.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class CarModel
    {
        public Guid Id { get; set; }

        public long BrandNameCode { get; set; }
        //[NotMapped]
        //public virtual DCarsBrand DCarsBrands { get; set; }

        public long ModelNameCode { get; set; }
        //[NotMapped]
        //public virtual DCarsModel DCarsModel { get; set; }

        public long ModelTypeCode { get; set; }
        //[NotMapped]
        //public virtual DType DTypes { get; set; }

        public long ModelColorCode { get; set; }
        //[NotMapped]
        //public virtual DColor DColors { get; set; }

    }
}
