
using dTax.Entity.Models.Cabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRepository : IBaseRepository<CabEntity>
    {
        bool IsExists(string LicensePlate, string VIN);
        CabEntity GetCabById(Guid Id);
        CabEntity GetCabByDriverId(Guid DriverId);
    }
}
