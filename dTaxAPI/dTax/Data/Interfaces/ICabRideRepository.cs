
using dTax.Entity.Models.CabRides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabRideRepository : IBaseRepository<CabRide>
    {
        CabRide GetCabRideById(Guid Id);
        IEnumerable<CabRide> GetCabRideList();
        bool ActiveBookExist(Guid Id);
        void UpdateEntity(CabRide entity);

    }
}
