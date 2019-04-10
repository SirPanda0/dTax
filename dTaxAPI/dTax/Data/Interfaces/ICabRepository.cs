
using dTax.Entity.Models.Cabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRepository : IBaseRepository<Cab>
    {
        bool IsExists(string LicensePlate, string VIN);
        Cab GetCabById(Guid Id);
        Cab GetCabByDriverId(Guid DriverId);
    }
}
