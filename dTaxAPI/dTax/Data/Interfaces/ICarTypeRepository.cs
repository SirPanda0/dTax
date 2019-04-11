using dTax.Data.Repository;
using dTax.Entity.Models.CarTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICarTypeRepository : IBaseRepository<CarType>
    {
        IEnumerable<CarTypeView> GetListTypes();
    }
}
