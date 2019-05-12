
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ResponseModels
{
    public class DriverResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public long DrivingLicence { get; set; } //номер прав
        public DateTime ExpiryDate { get; set; } //дата окончания действия прав
        //Паспорт
        public DateTime RegistrationDate { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }
        //Авто
        public string LicensePlate { get; set; }
        public string VIN { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public string CarType { get; set; }

        public Guid[] DriverFilesIds { get; set; }
        public Guid[] CabFilesIds { get; set; }

        //public ICollection<\> DriverFileStorageId { get; set; }

       
        
    }
}
