using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarTypeRepository : BaseRepository<CarType>, ICarTypeRepository
    {
        public CarTypeRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
