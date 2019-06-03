using dTax.Data.Repository;
using dTax.Entity.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICarModelRepository : IBaseRepository<CarModelEntity>
    {
        IEnumerable<CarModelView> GetListModelsByBrandId(Guid brandid);
    }
}
