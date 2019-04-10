using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarModelRepository : BaseRepository<CarModel>, ICarModelRepository
    {
        public CarModelRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
