
using dTax.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        User FindUserLogin(string Email, string Password);
        bool IsUserExists(string Email);
        User FindUserEmail(string Email);
        User GetUserById(Guid Id);
        string GetUserFioById(Guid Id);
        void IsFullReg(Guid UserId);
        void IsHalfReg(Guid UserId);
    }
}
