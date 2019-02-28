﻿using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IDriverRepository : IBaseRepository<Driver>
    {
        
        Driver GetDriverByFileId(Guid fileId);
        Driver GetDriverById(Guid Id);

    }
}
