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
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }

        public DbPostrgreContext(DbContextOptions<DbPostrgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                Initialization();

        }

        private void Initialization()
        {
            //throw new NotImplementedException();
            
            /*
              Основные сущности:

                Секция 1(всё что связано с водителями):
                driver -
                shift -
                cab -
                car model -

                Секция 2(поездки):
                cab_ride - 
                cab_ride_status -

                Секция 3 (пользователи):

             */
        }
    }

    

}
