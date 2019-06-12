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

        public CabRideEntity GetCabRideByCustomerId(Guid Id)
        {
            return GetCabRideByCustomerIdAsync(Id).Result;
        }

        private async Task<CabRideEntity> GetCabRideByCustomerIdAsync(Guid id)
        {
            return await GetQuery()
                .Include(_=>_.CabRideStatusLink)
                    .ThenInclude(_=>_.Shift)
                 .Include(_ => _.CabRideStatusLink)
                    .ThenInclude(s=>s.Status)
                .Include(p=>p.PaymentType)
                .FirstOrDefaultAsync(_ => _.CustomerId == id && _.CabRideStatusLink.FirstOrDefault().Status.StatusNumber != 4 && _.CabRideStatusLink.FirstOrDefault().Status.StatusNumber != 1);
        }

        public CabRideEntity GetCabRideByDriverId(Guid Id)
        {
            return GetCabRideByDriverIdAsync(Id).Result;
        }

        private async Task<CabRideEntity> GetCabRideByDriverIdAsync(Guid id)
        {
            return await GetQuery()
                .Include(_ => _.CabRideStatusLink)
                    .ThenInclude(_=>_.Shift)
                .Include(_ => _.CabRideStatusLink)
                    .ThenInclude(_=>_.Status)
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(_ => _.CabRideStatusLink.FirstOrDefault().Shift.DriverId == id && _.CabRideStatusLink.FirstOrDefault().Status.StatusNumber != 4 && _.CabRideStatusLink.FirstOrDefault().Status.StatusNumber != 1);
        }

        private async Task<CabRideEntity> GetCabRideByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public IEnumerable<CabRideEntity> GetCabRideList(int page, int size)
        {
            return GetCabRideListAsync(page, size).Result;
        }

        private async Task<IEnumerable<CabRideEntity>> GetCabRideListAsync(int page, int size)
        {
            return await GetQuery()
                .Include(l => l.CabRideStatusLink)
                    .ThenInclude(_ => _.Status)
                .Where(_ => _.IsCanceled != true && _.CabRideStatusLink.FirstOrDefault().Status.StatusNumber == 1)
                .Skip(size * (page - 1))
                .Take(size)
                .ToListAsync();
        }


        public bool ActiveBookExist(Guid Id)
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
