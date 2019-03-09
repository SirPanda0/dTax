﻿using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface ICabRideStatusRepository : IBaseRepository<CabRideStatus>
    {
        CabRideStatus GetCarModelById(Guid Id);
    }
}
