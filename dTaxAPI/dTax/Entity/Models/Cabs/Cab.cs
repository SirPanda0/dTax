using dTax.Entity.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models.CarBrands;
using dTax.Entity.Models.CarModels;
using dTax.Entity.Models.CarColors;
using dTax.Entity.Models.CarTypes;
using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.Many;

namespace dTax.Entity.Models.Cabs
{

    public class Cab : BaseEntity
    {


        //номера автомобиля
        public string LicensePlate { get; set; }

        //VIN авто
        public string VIN { get; set; }

        //id модели авто
        public Guid CarBrandId { get; set; }
        public CarBrand CarBrand { get; set; }

        public Guid CarModelId { get; set; }
        public CarModel CarModel { get; set; }

        public Guid CarColorId { get; set; }
        public CarColor CarColor { get; set; }

        public Guid CarTypeId { get; set; }
        public CarType CarType { get; set; }

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

        
        public ICollection<FilesToCab> FileLink { get; set; }

        //Проверен ли автомобиль оператором
        //public bool Сonfirmed { get; set; }

    }
}
