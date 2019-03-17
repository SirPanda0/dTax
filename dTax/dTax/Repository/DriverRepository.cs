using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class DriverRepository : BaseRepository<Driver>, IDriverRepository
    {
        public DriverRepository(DbPostrgreContext context) : base(context)
        {
        }

        public Driver GetDriverById(Guid Id)
        {
            return GetDriverByIdAsync(Id).Result;
        }

        private async Task<Driver> GetDriverByIdAsync(Guid id)
        {
            return await this.GetQuery().FirstOrDefaultAsync(_ => _.IsDeleted == false && _.Id == id);
        }

        public Driver GetDriverByUserId(Guid Id)
        {
            return GetDriverByUserIdAsync(Id).Result;
        }

        private async Task<Driver> GetDriverByUserIdAsync(Guid id)
        {
            return await this.GetQuery().FirstOrDefaultAsync(_ => _.IsDeleted == false && _.UserId == id);
        }


        public Driver GetDriverByFileId(Guid fileId)
        {
            return GetDriverByFileIdAsync(fileId).Result;
        }

        private async Task<Driver> GetDriverByFileIdAsync(Guid fileId)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.FileLink.FirstOrDefault().FileId == fileId);
        }

        public IEnumerable<Driver> GetListDrivers()
        {
            return GetListDriversAsync().Result;
        }

        private async Task<IEnumerable<Driver>> GetListDriversAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true && _.Сonfirmed != false).ToListAsync();
        }

        public IEnumerable<Driver> GetUnconfirmedDrivers()
        {
            return GetUnconfirmedDriversAsync().Result;
        }

        private async Task<IEnumerable<Driver>> GetUnconfirmedDriversAsync()
        {
            return await GetQuery().Where(_ => _.IsDeleted != true && _.Сonfirmed != true).ToListAsync();
        }


        public bool IsExists(Guid userid, long DrivingLicence, string PassportSerial, string PassportNumber)
        {
            var user = GetQuery()
                .FirstOrDefault(_ => _.DrivingLicence == DrivingLicence
                || _.PassportSerial == PassportSerial || _.PassportNumber == PassportNumber);
            if (user == null)
                return false;
            return true;
        }

        public Driver IsConfirmed(Guid DriverId)
        {
            Driver driver = GetDriverById(DriverId);
            driver.Сonfirmed = true;
            Update(driver);
            Commit();
            return driver;
        }


    }
}
