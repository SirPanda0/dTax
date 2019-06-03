using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CarTypeRepository : BaseRepository<CarTypeEntity>, ICarTypeRepository
    {
        public CarTypeRepository(DbPostrgreContext context) : base(context)
        {
        }

        public IEnumerable<CarTypeView> GetListTypes()
        {
            return GetListTypesAsync().Result;
        }

        private async Task<IEnumerable<CarTypeView>> GetListTypesAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true).Select(_ =>
                new CarTypeView()
                {
                    Id = _.Id,
                    Name = _.Name
                })
                .OrderBy(n => n.Name)
                .ToListAsync();
        }
    }
}
