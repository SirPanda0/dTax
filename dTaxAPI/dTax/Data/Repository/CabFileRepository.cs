using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Many;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class CabFileRepository : BaseRepository<FilesToCab>, ICabFileRepository
    {
        public CabFileRepository(DbPostrgreContext context) : base(context)
        {
        }

        public bool Exist(Guid FileId)
        {
           var exist = GetQuery().FirstOrDefault(_=>_.FileId == FileId);
            if (exist != null)
                return true;
            else
                return false;
        }

        public void AddLinkCab(Guid CabId, Guid FileId)
        {
            Insert(new FilesToCab
            {
                CabId = CabId,
                FileId = FileId
            });
            Commit();
        }
    }
}
