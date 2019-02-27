using dTax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Interfaces.Repository
{
    public interface IUserRepository: IBaseRepository<User>
    {
        User FindUserLogin(string Email, string Password);
        bool IsUserExists(string Email);
        User FindUserEmail(string Email);
    }
}
