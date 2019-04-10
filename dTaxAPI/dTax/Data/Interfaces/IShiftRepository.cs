
using dTax.Entity.Models.Shifts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IShiftRepository : IBaseRepository<Shift>
    {
        Shift GetShiftByDriverId(Guid Id);
    }
}
