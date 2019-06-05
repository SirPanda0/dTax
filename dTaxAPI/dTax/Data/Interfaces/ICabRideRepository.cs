
using dTax.Entity.Models.CabRides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRideRepository : IBaseRepository<CabRideEntity>
    {
        CabRideEntity GetCabRideById(Guid Id);
        IEnumerable<CabRideEntity> GetCabRideList(int page, int size);
        bool ActiveBookExist(Guid Id);
        void UpdateEntity(CabRideEntity entity);
        CabRideEntity GetCabRideByCustomerId(Guid id);
        CabRideEntity GetCabRideByDriverId(Guid id);

    }
}
