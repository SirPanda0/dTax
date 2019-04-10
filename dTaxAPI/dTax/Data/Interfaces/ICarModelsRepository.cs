
using dTax.Entity.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICarModelsRepository : IBaseRepository<CarModel>
    {
        CarModel GetCarModelById(Guid Id);
    }
}
