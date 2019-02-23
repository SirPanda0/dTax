using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    interface IRoleRepository
    {
        string FindNameById(int id);
        int FindIdByName(string name);
    }
}
