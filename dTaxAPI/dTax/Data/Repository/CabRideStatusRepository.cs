using dTax.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models.CabRideStatuses;
using dTax.Entity;
using dTax.Common.Enums;

namespace dTax.Data.Repository
{
    public class CabRideStatusRepository : BaseRepository<CabRideStatusEntity>, ICabRideStatusRepository
    {
        public CabRideStatusRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CabRideStatusEntity GetCabRideStatusByRideId(Guid Id)
        {
            return GetCabRideStatusByRideIdAsync(Id).Result;
        }

        private async Task<CabRideStatusEntity> GetCabRideStatusByRideIdAsync(Guid id)
        {
            return await GetQuery()
                .Include(ride => ride.CabRide)
                .FirstOrDefaultAsync(_ => _.CabRideId == id);
        }

        public CabRideStatusEntity GetCarModelById(Guid Id)
        {
            return GetCabRideByIdAsync(Id).Result;
        }

        private async Task<CabRideStatusEntity> GetCabRideByIdAsync(Guid id)
        {
            return await GetQuery()
                .Include(ride => ride.CabRide)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public void UpdateEntity(CabRideStatusEntity entity)
        {
            Update(entity);
            Commit();
        }

        public IEnumerable<CabRideStatusEntity> GetCabRideList()
        {
            return GetCabRideListAsync().Result;
        }

        private async Task<IEnumerable<CabRideStatusEntity>> GetCabRideListAsync()
        {
            return await GetQuery()
                .Where(_ => _.StatusId != (int)RideStatusEnum.Canceled && _.StatusId != (int)RideStatusEnum.Ended && _.StatusId != (int)RideStatusEnum.New)
                .ToListAsync();
        }

    }
}
