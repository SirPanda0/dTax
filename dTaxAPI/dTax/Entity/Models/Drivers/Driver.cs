using dTax.Entity.Models.Base;
using dTax.Entity.Models.Many;
using dTax.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Entity.Models.Drivers
{
    public class Driver : BaseEntity
    {
        
        

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
        //public long FileStorageId { get; set; }
        [NotMapped]
        public virtual ICollection<FilesToDriver> FileLink { get; set; }

        public bool Working { get; set; } = false;

        //Проверены ли документы водителя и его авто
        public bool Сonfirmed { get; set; } = false;

        //TODO
        //public float? geo_lat { get; set; }
        //public float? geo_long { get; set; }

    }
}
