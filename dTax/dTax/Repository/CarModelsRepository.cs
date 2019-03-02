using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CarModelsRepository : BaseRepository<CarModel>, ICarModelsRepository
    {
        public CarModelsRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CarModel GetCarModelById(Guid Id)
        {
            return GetCarModelByIdAsync(Id).Result;
        }

        private async Task<CarModel> GetCarModelByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }
    }
}
