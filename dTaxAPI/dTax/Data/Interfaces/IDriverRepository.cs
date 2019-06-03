
using dTax.Entity.Models.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IDriverRepository : IBaseRepository<DriverEntity>
    {
        
        DriverEntity GetDriverByFileId(Guid fileId);
        DriverEntity GetDriverById(Guid Id);
        bool IsExists(Guid userid, long DrivingLicence, string PassportSerial, string PassportNumber);
        IEnumerable<DriverEntity> GetUnconfirmedDrivers();
        DriverEntity IsConfirmed(Guid DriverId);
        DriverEntity IsUnConfirmed(Guid DriverId);
        IEnumerable<DriverEntity> GetListDrivers();
        DriverEntity GetDriverByUserId(Guid Id);


    }
}
