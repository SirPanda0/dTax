﻿ using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Shifts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class ShiftRepository : BaseRepository<ShiftEntity>, IShiftRepository
    {
        public ShiftRepository(DbPostrgreContext context) : base(context)
        {
        }

        public ShiftEntity GetShiftByDriverId (Guid Id)
        {
            return GetQuery().FirstOrDefault(_ => _.DriverId == Id);
        }

       


    }
}
