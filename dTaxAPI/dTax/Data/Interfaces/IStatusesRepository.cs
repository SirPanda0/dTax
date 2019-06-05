
using dTax.Entity.Models.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IStatusesRepository : IBaseRepository<StatusEntity>
    {
        IEnumerable<StatusEntity> GetStatuses();
    }
}
