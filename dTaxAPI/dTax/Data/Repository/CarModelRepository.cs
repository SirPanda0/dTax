using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarModels;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<CarModelView> GetListModelsByBrandId(Guid brandid)
        {
            return GetListModelsByBrandIdAsync(brandid).Result;
        }

        private async Task<IEnumerable<CarModelView>> GetListModelsByBrandIdAsync(Guid brandid)
        {
            return await GetQuery().Where(_ => _.IsDeleted != true && _.CarBrandId == brandid).Select(_ =>
                new CarModelView()
                {
                    Id = _.Id,
                    Name = _.Name
                })
                .OrderBy(n => n.Name)
                .ToListAsync();
        }
    }
}
