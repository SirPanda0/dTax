using dTax.Models.Many;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [JsonIgnore]
        public Driver Driver { get; set; }

        //Фото Авто
        //public Guid FileStorageId { get; set; }
        //public FileStorage FileStorage { get; set; }

        public bool Active { get; set; } = false;

        [NotMapped]
        public virtual ICollection<FilesToCab> FileLink { get; set; }

        //Проверен ли автомобиль оператором
        //public bool Сonfirmed { get; set; }

    }
}
