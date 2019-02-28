using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class CabRepository: BaseRepository<Cab>, ICabRepository
    {
        public CabRepository(DbPostrgreContext context) : base(context)
        {
        }



    }
}
