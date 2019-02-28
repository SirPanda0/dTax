using dTax.Interfaces.Repository;
using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class StatusesRepository: BaseRepository<Status>, IStatusesRepository
    {
        public StatusesRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
