using dTax.Entity.Models.Base;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CarBrands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CarModels
{

    public class CarModel : BaseEntity
    {
        public ICollection<Cab> Cabs { get; set; }

        public Guid CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }

        public string Name { get; set; }

    }
}
