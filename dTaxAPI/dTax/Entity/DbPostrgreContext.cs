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
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public DbSet<DriverEntity> Drivers { get; set; } //Водители
        public DbSet<CarModelEntity> CarModels { get; set; } //Модели зарегистрированных автомобилей
        public DbSet<CabEntity> Cabs { get; set; } //зарегистрированные автомобили
        public DbSet<ShiftEntity> Shifts { get; set; } //Смена

        public DbSet<CabRideEntity> CabRides { get; set; } //Потенциальные поездки
        public DbSet<PaymentTypeEntity> PaymentTypes { get; set; } //Тип оплаты: карта/наличные
        public DbSet<CabRideStatusEntity> CabRideStatuses { get; set; } //Статус поездки
        public DbSet<CustomerEntity> Customers { get; set; } //Заказчик
        public DbSet<StatusEntity> Statuses { get; set; } // Состояния поездки: новая/назначена/началась/завершилась/отменена

        public DbSet<FileStorageEntity> FileStorage { get; set; } //Хранилище файлов

        public DbSet<FileContentEntity> FileContents { get; set; }

        public DbSet<DriverFileEntity> FilesToDrivers { get; set; }

        public DbSet<CabFileEntity> FilesToCab { get; set; }

        public DbSet<CarBrandEntity> CarBrands { get; set; }

        public DbSet<CarColorEntity> CarColors { get; set; }

        public DbSet<CarTypeEntity> CarTypes { get; set; }


        public DbPostrgreContext(DbContextOptions<DbPostrgreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
               InitialInitialization();

        }

        private void InitialInitialization()
        {

            List<RoleEntity> roles = new List<RoleEntity>()
            {
                new RoleEntity() { Name = "Operator" },
                new RoleEntity() { Name = "User" },
                new RoleEntity() { Name = "Driver" }
            };

            List<StatusEntity> statuses = new List<StatusEntity>()
            {
                new StatusEntity() { StatusName = "Новая поездка", StatusNumber = 1},
                new StatusEntity() { StatusName = "Поездка, назначена водителю", StatusNumber =2},
                new StatusEntity() { StatusName = "Поездка началась", StatusNumber =3},
                new StatusEntity() { StatusName = "Поездка завершилась", StatusNumber =4},
                new StatusEntity() { StatusName = "Поездка отменена", StatusNumber =5},
                new StatusEntity() { StatusName = "Водитель прибыл", StatusNumber =6}
            };

            List<PaymentTypeEntity> payments = new List<PaymentTypeEntity>()
            {
                new PaymentTypeEntity() { TypeName = "Наличные"},
                new PaymentTypeEntity() { TypeName = "Карта"}
            };

            List<UserEntity> user = new List<UserEntity>()
            {
                new UserEntity()
                {
                    Email = "dTax-mailing@yandex.ru",
                    Password = "cb891b06ce3c1981cc00a1966ddfe98228011de9c582823ecdb2ccfe127a20df", //TODO hash этого пароля M9S206
                    Role = roles[0],
                    BirthDate = DateTime.Now,
                    FirstName = "Админ",
                    LastName = "Админович",
                    IsFullReg = true
                },
                new UserEntity()
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
