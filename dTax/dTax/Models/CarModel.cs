using dTax.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class CarModel
    {
        public Guid Id { get; set; }

        public long BrandNameCode { get; set; }
        public virtual ICollection<DCarsBrand> DCarsBrands { get; set; }

        public long ModelNameCode { get; set; }
        public virtual ICollection<DCarsModel> DCarsModel { get; set; }

        public long ModelTypeCode { get; set; }
        public virtual ICollection<DType> DTypes { get; set; }

        public long ModelColorCode { get; set; }
        public virtual ICollection<DColor> DColors { get; set; }

    }
}
