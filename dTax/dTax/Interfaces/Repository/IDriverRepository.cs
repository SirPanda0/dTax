using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        
        Driver GetDriverByFileId(long fileId);
        Driver GetDriverById(Guid Id);
        bool IsExists(Guid userid, long DrivingLicence, string PassportSerial, string PassportNumber);
        IEnumerable<Driver> GetUnconfirmedDrivers();
        Driver IsConfirmed(Guid DriverId);
        IEnumerable<Driver> GetListDrivers();

    }
}
