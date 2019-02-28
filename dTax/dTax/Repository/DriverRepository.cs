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



        public Driver GetDriverByFileId(Guid fileId)
        {
            return GetDriverByFileIdAsync(fileId).Result;
        }

        private async Task<Driver> GetDriverByFileIdAsync(Guid fileId)
        {
            return await GetQuery().FirstOrDefaultAsync(_ => _.FileStorage.FileId == fileId);
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


    }
}
