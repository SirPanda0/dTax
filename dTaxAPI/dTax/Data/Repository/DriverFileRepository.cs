 using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.Many;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class DriverFileRepository : BaseRepository<FilesToDriver>, IDriverFileRepository
    {
        public DriverFileRepository(DbPostrgreContext context) : base(context)
        {
        }

        public bool Exist(Guid FileId)
        {
            var exist = GetQuery().FirstOrDefault(_ => _.FileId == FileId);
            if (exist != null)
                return true;
            else
                return false;
        }

        public void AddLinkDriver(Guid DriverId, Guid FileId)
        {
            Insert(new FilesToDriver
            {
                DriverId = DriverId,
                FileId = FileId
            });
            Commit();
        }

    }
}
