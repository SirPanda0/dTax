
using dTax.Entity.Models.CabRideStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRideStatusRepository : IBaseRepository<CabRideStatusEntity>
    {
        CabRideStatusEntity GetCarModelById(Guid Id);
        CabRideStatusEntity GetCabRideStatusByRideId(Guid Id);
        void UpdateEntity(CabRideStatusEntity entity);
        IEnumerable<CabRideStatusEntity> GetCabRideList();
    }
}
