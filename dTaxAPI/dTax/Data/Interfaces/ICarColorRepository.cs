using dTax.Data.Repository;
using dTax.Entity.Models.CarColors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICarColorRepository : IBaseRepository<CarColor>
    {
    }
}
