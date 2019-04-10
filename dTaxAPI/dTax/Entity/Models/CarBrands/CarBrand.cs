using dTax.Entity.Models.Base;
using dTax.Entity.Models.Cabs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CarBrands
{
    public class CarBrand : BaseEntity
    {
        public ICollection<Cab> Cabs { get; set; }

        public Guid CarBrandId { get; set; }
        public string Name { get; set; }
    }
}
