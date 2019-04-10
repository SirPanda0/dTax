
using dTax.Entity.Models.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        
        Driver GetDriverByFileId(Guid fileId);
        Driver GetDriverById(Guid Id);
        bool IsExists(Guid userid, long DrivingLicence, string PassportSerial, string PassportNumber);
        IEnumerable<Driver> GetUnconfirmedDrivers();
        Driver IsConfirmed(Guid DriverId);
        IEnumerable<Driver> GetListDrivers();
        Driver GetDriverByUserId(Guid Id);

    }
}
