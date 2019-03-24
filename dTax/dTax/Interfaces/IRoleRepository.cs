using dTax.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IRoleRepository
    {
        string FindNameById(int id);
        int FindIdByName(string name);
    }
}
