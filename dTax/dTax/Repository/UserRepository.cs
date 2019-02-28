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

        public User FindUserEmail(string Email)
        {
            return GetQuery().FirstOrDefault(_ => _.Email == Email);
        }
    }
}
