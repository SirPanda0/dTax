﻿using dTax.Entity.Models.Base;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CarBrands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.CarModels
{

    public class CarModelEntity : BaseEntity
    {
        public ICollection<CabEntity> Cabs { get; set; }

        public Guid CarBrandId { get; set; }
        public CarBrandEntity CarBrand { get; set; }

        public string Name { get; set; }

    }
}