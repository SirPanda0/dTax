 using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class StatusesRepository: BaseRepository<StatusEntity>, IStatusesRepository
    {
        public StatusesRepository(DbPostrgreContext context) : base(context)
        {
        }
    }
}
