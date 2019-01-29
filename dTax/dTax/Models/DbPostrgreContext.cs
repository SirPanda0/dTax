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

        public DbSet<Driver> Drivers { get; set; } //Водители
        public DbSet<CarModel> CarModels { get; set; } //Модели зарегистрированных автомобилей
        public DbSet<Cab> Cabs { get; set; } //зарегистрированные автомобили
        public DbSet<Shift> Shifts { get; set; } //Смена

        public DbSet<CabRide> CabRides { get; set; } //Потенциальные поездки
        public DbSet<PaymentType> PaymentTypes { get; set; } //Тип оплаты: карта/наличные
        public DbSet<CabRideStatus> CabRideStatuses { get; set; } //Статус поездки
        public DbSet<Customer> Customers { get; set; } //Заказчик
        public DbSet<Status> Statuses { get; set; } // Состояния поездки: новая/назначена/началась/завершилась/отменена


        public DbPostrgreContext(DbContextOptions<DbPostrgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                InitializationAsync();

        }

        private async Task InitializationAsync()
        {

            Role role = new Role() { Name = "Admin" };
            User user = new User()
            {
                Login = "Admin",
                Password = "Admin",
                Role = role,
                BirthDate = DateTime.Now.Date,
                FirstName = "Админ",
                LastName = "Админович"
            };

            Roles.Add(role);
            Users.Add(user);
            await SaveChangesAsync();

            //throw new NotImplementedException();
            
            /*
              Основные сущности:

                Секция 1(всё что связано с водителями):
                driver (водитель) -
                shift (смена) -
                cab (сам такси) -
                car model(модель авто) -

                Секция 2(поездки):
                cab_ride - 
                cab_ride_status -

                Секция 3 (пользователи):

             */
        }
    }

    

}
