using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarColorRepository : BaseRepository<CarColor>, ICarColorRepository
    {
        public CarColorRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
