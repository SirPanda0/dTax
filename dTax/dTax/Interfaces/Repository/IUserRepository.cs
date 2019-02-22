using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    interface IUserRepository
    {
        User FindUserLogin(string Email, string Password);
        bool IsUserExists(string Email);
    }
}
