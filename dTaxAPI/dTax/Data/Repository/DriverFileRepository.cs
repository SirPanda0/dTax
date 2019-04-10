 using dTax.Data.Interfaces;
using dTax.Entity;
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
