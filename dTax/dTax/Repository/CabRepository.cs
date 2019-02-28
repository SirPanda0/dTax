using dTax.Interfaces.Repository;
using dTax.Models;
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
