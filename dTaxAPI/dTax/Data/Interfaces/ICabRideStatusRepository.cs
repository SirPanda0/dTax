
using dTax.Entity.Models.CabRideStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRideStatusRepository : IBaseRepository<CabRideStatus>
    {
        CabRideStatus GetCarModelById(Guid Id);
        CabRideStatus GetCabRideStatusByRideId(Guid Id);
        void UpdateEntity(CabRideStatus entity);
        IEnumerable<CabRideStatus> GetCabRideList();
    }
}
