using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CarColors;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<CarColorView> GetListColors()
        {
            return GetListColorsAsync().Result;
        }

        private async Task<IEnumerable<CarColorView>> GetListColorsAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true).Select(_ =>
                new CarColorView()
                {
                    Id = _.Id,
                    Name = _.Name
                })
                .OrderBy(n => n.Name)
                .ToListAsync();
        }
    }
}
