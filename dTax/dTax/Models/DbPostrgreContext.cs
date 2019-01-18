using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dTax.Models
{
    public class DbPostrgreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbPostrgreContext(DbContextOptions<DbPostrgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                Initialization();

        }

        private void Initialization()
        {
            throw new NotImplementedException();
        }
    }

    

}
