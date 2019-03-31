using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRepository : BaseRepository<Cab>, ICabRepository
    {
        public CabRepository(DbPostrgreContext context) : base(context)
        {
        }

        public Cab GetCabById(Guid Id)
        {
            return GetCabByIdAsync(Id).Result;
        }

        private async Task<Cab> GetCabByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Cab GetCabByDriverId(Guid DriverId)
        {
            return GetCabByDriverIdAsync(DriverId).Result;
        }

        private async Task<Cab> GetCabByDriverIdAsync(Guid DriverId)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.DriverId == DriverId);
        }

        public bool IsExists(string LicensePlate, string VIN)
        {
            var cab = GetQuery()
                .FirstOrDefault(_ => _.LicensePlate == LicensePlate || _.VIN == VIN);
            if (cab == null)
                return false;
            return true;
        }


    }
}
