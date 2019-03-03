using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRideRepository : BaseRepository<CabRide>, ICabRideRepository
    {
        public CabRideRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CabRide GetCabRideById(Guid Id)
        {
            return GetCabRideByIdAsync(Id).Result;
        }

        private async Task<CabRide> GetCabRideByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public IEnumerable<CabRide> GetCabRideList()
        {
            return GetCabRideListAsync().Result;
        }

        private async Task<IEnumerable<CabRide>> GetCabRideListAsync()
        {
            return await GetQuery().Where(_ => _.Canceled != true).ToListAsync();
        }

    }
}
