using dTax.Data.Repository;
using dTax.Entity.Models.CarBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICarBrandRepository : IBaseRepository<CarBrand>
    {
        IEnumerable<CarBrandView> GetListBrands();
    }
}
