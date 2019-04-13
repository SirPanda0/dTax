﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IRoleRepository
    {
        string FindNameById(int id);
        int FindIdByName(string name);
    }
}