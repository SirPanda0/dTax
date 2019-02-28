using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRideStatusRepository : BaseRepository<CabRide>, ICabRideStatusRepository
    {
        public CabRideStatusRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
