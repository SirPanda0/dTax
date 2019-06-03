
using dTax.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Interfaces
{
    public interface IUserRepository: IBaseRepository<UserEntity>
    {
        UserEntity FindUserLogin(string Email, string Password);
        bool IsUserExists(string Email);
        UserEntity FindUserEmail(string Email);
        UserEntity GetUserById(Guid Id);
        string GetUserFioById(Guid Id);
        void IsFullReg(Guid UserId);
        void IsHalfReg(Guid UserId);
    }
}
