using dTax.Data.Interfaces;
using dTax.Entity;
using dTax.Entity.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbPostrgreContext context) : base(context)
        {
        }

        public User FindUserLogin(string Email, string password)
        {
            return GetQuery()
                 .Include(_ => _.Role)
                 .FirstOrDefault<User>(
                _ => _.Email == Email && _.Password == password);
        }

        public bool IsUserExists(string Email)
        {
            var user = GetQuery().FirstOrDefault(_ => _.Email == Email);
            if (user == null)
                return false;
            return true;
        }

        public void IsFullReg(Guid UserId)
        {
            User user = GetUserByIdAsync(UserId).Result;
            user.IsFullReg = true;
            Update(user);
            Commit();
        }

        public void IsHalfReg(Guid UserId)
        {
            User user = GetUserByIdAsync(UserId).Result;
            user.IsFullReg = false;
            Update(user);
            Commit();
        }


        public User FindUserEmail(string Email)
        {
            return GetQuery().FirstOrDefault(_ => _.Email == Email);
        }

        public User GetUserById(Guid Id)
        {
            return GetUserByIdAsync(Id).Result;
        }

        public string GetUserFioById(Guid Id)
        {
            var user = GetUserByIdAsync(Id).Result;

            return String.Format("{0} {1} {2}", user.FirstName, user.MiddleName, user.LastName);
        }

        private async Task<User> GetUserByIdAsync(Guid id)
        {
            return await this.GetQuery()
                .FirstOrDefaultAsync(_ => _.IsDeleted == false && _.Id == id);
        }


    }
}
