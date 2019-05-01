using dTax.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface ICabFileRepository : IBaseRepository<FilesToCab>
    {
        void AddLinkCab(Guid CabId, Guid FileId);
        bool Exist(Guid FileId);
    }
}
