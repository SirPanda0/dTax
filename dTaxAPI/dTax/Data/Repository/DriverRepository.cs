﻿ using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class DriverRepository : BaseRepository<DriverEntity>, IDriverRepository
    {
        public DriverRepository(DbPostrgreContext context) : base(context)
        {
        }

        public DriverEntity GetDriverById(Guid Id)
        {
            return GetDriverByIdAsync(Id).Result;
        }

        private async Task<DriverEntity> GetDriverByIdAsync(Guid id)
        {
            return await this.GetQuery()
                .Include(f=>f.FileLink)
                .Include(u=>u.User)
                .FirstOrDefaultAsync(_ => _.IsDeleted == false && _.Id == id);
        }

        public DriverEntity GetDriverByUserId(Guid Id)
        {
            return GetDriverByUserIdAsync(Id).Result;
        }

        private async Task<DriverEntity> GetDriverByUserIdAsync(Guid id)
        {
            return await this.GetQuery()
                .Include(f => f.FileLink)
                .Include(u => u.User)
                .FirstOrDefaultAsync(_ => _.IsDeleted == false && _.UserId == id);
        }


        public DriverEntity GetDriverByFileId(Guid fileId)
        {
            return GetDriverByFileIdAsync(fileId).Result;
        }

        private async Task<DriverEntity> GetDriverByFileIdAsync(Guid fileId)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.FileLink.FirstOrDefault().FileId == fileId);
        }

        public IEnumerable<DriverEntity> GetListDrivers()
        {
            return GetListDriversAsync().Result;
        }

        private async Task<IEnumerable<DriverEntity>> GetListDriversAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true && _.IsСonfirmed != false).ToListAsync();
        }

        public IEnumerable<DriverEntity> GetUnconfirmedDrivers()
        {
            var result = GetUnconfirmedDriversAsync().Result;

            return result;
        }

        private async Task<IEnumerable<DriverEntity>> GetUnconfirmedDriversAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true && _.IsСonfirmed != true).ToListAsync();
        }


        public bool IsExists(Guid userid, long DrivingLicence, string PassportSerial, string PassportNumber)
        {
            var user = GetQuery()
                .FirstOrDefault(_ => _.DrivingLicence == DrivingLicence && _.Id != userid
                || _.PassportSerial == PassportSerial && _.Id != userid || _.PassportNumber == PassportNumber && _.Id != userid);
            if (user == null)
                return false;
            return true;
        }

        public DriverEntity IsConfirmed(Guid DriverId)
        {
            DriverEntity driver = GetDriverById(DriverId);
            driver.IsСonfirmed = true;
            Update(driver);
            Commit();
            return driver;
        }

        public DriverEntity IsUnConfirmed(Guid DriverId)
        {
            DriverEntity driver = GetDriverById(DriverId);
            driver.IsСonfirmed = false;
            Update(driver);
            Commit();
            return driver;
        }

    }
}
