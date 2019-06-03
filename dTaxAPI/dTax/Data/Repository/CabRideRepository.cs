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
    public class CabRideRepository : BaseRepository<CabRideEntity>, ICabRideRepository
    {
        public CabRideRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CabRideEntity GetCabRideById(Guid Id)
        {
            return GetCabRideByIdAsync(Id).Result;
        }

        private async Task<CabRideEntity> GetCabRideByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public IEnumerable<CabRideEntity> GetCabRideList()
        {
            return GetCabRideListAsync().Result;
        }

        private async Task<IEnumerable<CabRideEntity>> GetCabRideListAsync()
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

        public void UpdateEntity(CabRideEntity entity)
        {
            Update(entity);
            Commit();
        }

    }
}
