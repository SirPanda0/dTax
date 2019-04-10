 using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.CabRides;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
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
            return await GetQuery().Where(_ => _.IsCanceled != true).ToListAsync();
        }

        public bool ActiveBookExist (Guid Id)
        {
            var book = GetQuery().FirstOrDefault(_ => _.IsCanceled != true && _.CustomerId == Id);
            if (book == null)
                return false;
            return true;
        }

        public void UpdateEntity(CabRide entity)
        {
            Update(entity);
            Commit();
        }

    }
}
