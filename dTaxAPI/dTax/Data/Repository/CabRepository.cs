﻿ using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Cabs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CabRepository : BaseRepository<CabEntity>, ICabRepository
    {
        public CabRepository(DbPostrgreContext context) : base(context)
        {
        }

        public CabEntity GetCabById(Guid Id)
        {
            return GetCabByIdAsync(Id).Result;
        }

        private async Task<CabEntity> GetCabByIdAsync(Guid id)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.Id == id);
        }

        public CabEntity GetCabByDriverId(Guid DriverId)
        {
            return GetCabByDriverIdAsync(DriverId).Result;
        }

        private async Task<CabEntity> GetCabByDriverIdAsync(Guid DriverId)
        {
            return await GetQuery()
                .Include(b=>b.CarBrand)
                .Include(m => m.CarModel)
                .Include(t => t.CarType)
                .Include(c => c.CarColor)
                .Include(f=>f.FileLink)
                .FirstOrDefaultAsync(_ => _.DriverId == DriverId);
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
