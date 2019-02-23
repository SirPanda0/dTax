using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class Driver
    {
        
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        //Водительское удостоверение
        public long DrivingLicence { get; set; } //номер прав
        public DateTime ExpiryDate { get; set; } //дата окончания действия прав

        //Паспорт
        public DateTime RegistrationDate { get; set; }
        public string PassportSerial { get; set; }
        public string PassportNumber { get; set; }

        //Фото документов
        public Guid? FileStorageId { get; set; }
        public FileStorage FileStorage { get; set; }
        
        public bool Working { get; set; }

        //Проверены ли документы водителя и его авто
        public bool Сonfirmed { get; set; }

        //TODO
        //public float? geo_lat { get; set; }
        //public float? geo_long { get; set; }
    }
}
