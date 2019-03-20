using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Models.Dictionary;
using dTax.Models.Many;
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

        public DbSet<FileStorage> FileStorage { get; set; } //Хранилище файлов

        public DbSet<FilesToDriver> FilesToDrivers { get; set; }

        public DbSet<FilesToCab> FilesToCab { get; set; }

        public DbSet<DCarsBrand> DCarsBrands { get; set; }

        public DbSet<DCarsModel> DCarsModels { get; set; }

        public DbSet<DColor> DColors { get; set; }

        public DbSet<DType> DTypes { get; set; }



        public DbPostrgreContext(DbContextOptions<DbPostrgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
                InitialInitialization();

        }

        private void InitialInitialization()
        {

            List<Role> roles = new List<Role>()
            {
                new Role() { Name = "Operator" },
                new Role() { Name = "User" },
                new Role() { Name = "Driver" }
            };

            List<Status> statuses = new List<Status>()
            {
                new Status() { StatusName = "Новая поездка"},
                new Status() { StatusName = "Поездка, назначена водителю"},
                new Status() { StatusName = "Поездка началась"},
                new Status() { StatusName = "Поездка завершилась"},
                new Status() { StatusName = "Поездка отменена"},
            };

            List<PaymentType> payments = new List<PaymentType>()
            {
                new PaymentType() { TypeName = "Наличные"},
                new PaymentType() { TypeName = "Карта"}
            };

            List<User> user = new List<User>()
            {
                new User()
                {
                    Email = "dTax-mailing@yandex.ru",
                    Password = "cb891b06ce3c1981cc00a1966ddfe98228011de9c582823ecdb2ccfe127a20df", //TODO hash этого пароля M9S206
                    Role = roles[0],
                    BirthDate = DateTime.Now,
                    FirstName = "Админ",
                    LastName = "Админович",
                    FullReg = true
                },
                new User()
                {
                    Id = new Guid("73556bc9-522d-4c3c-a94b-707aa8e1d82b"),
                    Email = "string",
                    Password = "cb891b06ce3c1981cc00a1966ddfe98228011de9c582823ecdb2ccfe127a20df", //TODO hash этого пароля M9S206
                    Role = roles[2],
                    BirthDate = DateTime.Now,
                    FirstName = "Водитель",
                    LastName = "Водителевич",
                    FullReg = false
                }

            };

            



            Roles.AddRange(roles);
            Statuses.AddRange(statuses);
            PaymentTypes.AddRange(payments);
            Users.AddRange(user);
            
            SaveChanges();


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
