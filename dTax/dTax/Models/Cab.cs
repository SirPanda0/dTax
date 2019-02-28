using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{

    public class Cab : BaseEntity
    {


        //номера автомобиля
        public string LicensePlate { get; set; }

        //VIN авто
        public string VIN { get; set; }

        //id модели авто
        public Guid CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        //год выпуска
        public int ManufactureYear { get; set; }

        //id водителя
        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }

        //Фото Авто
        public Guid FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }

        public bool Active { get; set; } = false;

        //Проверен ли автомобиль оператором
        //public bool Сonfirmed { get; set; }

    }
}
