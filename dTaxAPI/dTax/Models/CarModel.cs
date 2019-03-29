using dTax.Models.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class CarModel : BaseEntity
    {
        public ICollection<Cab> Cabs { get; set; }

        public Guid CarModelId { get; set; }
        public CarBrand CarBrand  { get; set; }

       

    }
}
