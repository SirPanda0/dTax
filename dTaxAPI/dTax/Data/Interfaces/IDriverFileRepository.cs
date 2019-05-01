using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IDriverFileRepository : IBaseRepository<FilesToDriver>
    {
        void AddLinkDriver(Guid DriverId, Guid FileId);
        bool Exist(Guid FileId);
    }
}
