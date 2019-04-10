using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CabRides;
using dTax.Entity.Models.CabRideStatuses;
using dTax.Entity.Models.CarBrands;
using dTax.Entity.Models.CarColors;
using dTax.Entity.Models.CarModels;
using dTax.Entity.Models.CarTypes;
using dTax.Entity.Models.Customers;
using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.FileContents;
using dTax.Entity.Models.FileStorages;
using dTax.Entity.Models.Many;
using dTax.Entity.Models.PaymentTypes;
using dTax.Entity.Models.Roles;
using dTax.Entity.Models.Shifts;
using dTax.Entity.Models.Statuses;
using dTax.Entity.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace dTax.Entity
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

        public DbSet<FileContent> FileContents { get; set; }

        public DbSet<FilesToDriver> FilesToDrivers { get; set; }

        public DbSet<FilesToCab> FilesToCab { get; set; }

        public DbSet<CarBrand> CarBrands { get; set; }

        public DbSet<CarColor> CarColors { get; set; }

        public DbSet<CarType> CarTypes { get; set; }


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
                    IsFullReg = true
                },
                new User()
                {
                    Email = "hellpanda44@gmail.com",
                    Password = "cb891b06ce3c1981cc00a1966ddfe98228011de9c582823ecdb2ccfe127a20df", //TODO hash этого пароля M9S206
                    Role = roles[2],
                    BirthDate = DateTime.Now,
                    FirstName = "Иван",
                    MiddleName = "Александрович",
                    LastName = "Панагушин",
                    PhoneNumber = "89159287903",
                    IsFullReg = false
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
