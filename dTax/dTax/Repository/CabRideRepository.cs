using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRideRepository : BaseRepository<CabRide>, ICabRideRepository
    {
        public CabRideRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
