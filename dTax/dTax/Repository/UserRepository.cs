using dTax.Interfaces.Repository;
using dTax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        DbPostrgreContext CommonDbContext;

        public UserRepository(DbPostrgreContext commonDbContext) : base(commonDbContext)
        {
            CommonDbContext = commonDbContext; ;
        }

        public User FindUserLogin(string Email, string password)
        {
            return CommonDbContext.Users
                 .Include(_ => _.Role)
                 .FirstOrDefault<User>(
                _ => _.Email == Email && _.Password == password);
        }

        public bool IsUserExists(string Email)
        {
            var user = CommonDbContext.Users.FirstOrDefault(_ => _.Email == Email);
            if (user == null)
                return false;
            return true;
        }

        public User FindUserEmail(string Email)
        {
            return CommonDbContext.Users.FirstOrDefault(_ => _.Email == Email);
        }
    }
}
