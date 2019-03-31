using dTax.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface ICabFileRepository : IBaseRepository<FilesToCab>
    {
        void AddLinkCab(Guid CabId, Guid FileId);
    }
}
