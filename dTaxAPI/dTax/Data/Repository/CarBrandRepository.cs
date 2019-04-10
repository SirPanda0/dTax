using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarBrandRepository : BaseRepository<CarBrand>, ICarBrandRepository
    {
        public CarBrandRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
