using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class Cab
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } //номера автомобиля
        public int CarModelId { get; set; } //id модели авто
        public CarModel CarModel { get; set; }
        public int ManufactureYear { get; set; } //год выпуска
        public int DriverId { get; set; } //id водителя
        public Driver Driver { get; set; }
        public bool Active { get; set; }
    }
}
