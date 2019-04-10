using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Shifts
{
    public class Shift
    {

        public Guid Id { get; set; }


        public Guid DriverId { get; set; } //id водителя
        public Driver Driver { get; set; }

        public Guid CabId { get; set; } // id автомобиля
        public Cab Cab { get; set; }

        public DateTime LoginTime { get; set; } //время входа
        //public DateTime LogoutTime { get; set; } //время выхода
    }
}
