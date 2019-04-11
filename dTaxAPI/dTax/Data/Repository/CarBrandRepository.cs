using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarBrands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarBrandRepository : BaseRepository<CarBrand>, ICarBrandRepository
    {
        public CarBrandRepository(DbPostrgreContext context) : base(context)
        {
        }

        public IEnumerable<CarBrandView> GetListBrands()
        {
            return GetListBrandsAsync().Result;
        }

        private async Task<IEnumerable<CarBrandView>> GetListBrandsAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true).Select(_=>
                new CarBrandView()
                {
                    Id = _.Id,
                    Name = _.Name
                })
                .OrderBy(n=>n.Name)
                .ToListAsync();
        }
    }
}
