using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class ShiftRepository : BaseRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(DbPostrgreContext context) : base(context)
        {
        }

        public Shift GetShiftByDriverId (Guid Id)
        {
            return GetQuery().FirstOrDefault(_ => _.DriverId == Id);
        }

       


    }
}
