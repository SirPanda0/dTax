using dTax.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IDriverFileRepository : IBaseRepository<FilesToDriver>
    {
        void AddLinkDriver(Guid DriverId, Guid FileId);
    }
}
