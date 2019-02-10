using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public int DriverId { get; set; } //id водителя
        public Driver Driver { get; set; }

        public int CabId { get; set; } // id автомобиля
        public Cab Cab { get; set; }

        public DateTime LoginTime { get; set; } //время входа
        //public DateTime LogoutTime { get; set; } //время выхода
    }
}
