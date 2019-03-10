using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRideStatusRepository : BaseRepository<CabRideStatus>, ICabRideStatusRepository
    {
        public CabRideStatusRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CabRideStatus GetCabRideStatusByRideId(Guid Id)
        {
            return GetCabRideStatusByRideIdAsync(Id).Result;
        }

        private async Task<CabRideStatus> GetCabRideStatusByRideIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.CabRideId == id);
        }

        public CabRideStatus GetCarModelById(Guid Id)
        {
            return GetCabRideByIdAsync(Id).Result;
        }

        private async Task<CabRideStatus> GetCabRideByIdAsync(Guid id)
        {
            return await GetQuery()
                .Include(ride => ride.CabRide)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public void UpdateEntity(CabRideStatus entity)
        {
            Update(entity);
            Commit();
        }

    }
}
