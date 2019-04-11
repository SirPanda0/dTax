﻿using dTax.Entity.Models.Base;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CarModels;
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

        public ICollection<CarModel> CarModels { get; set; }

        public string Name { get; set; }
    }
}
