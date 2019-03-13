using dTax.Models;
using dTax.Models.Many;
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
        //public ICollection<\> DriverFileStorageId { get; set; }

        //Cab
        public Cab Cab { get; set; }
        public CarModel CarModel { get; set; }
        
    }
}
