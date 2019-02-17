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

        public long DrivingLicence { get; set; } //номер прав
        public DateTime ExpiryDate { get; set; } //дата окончания действия прав
        public bool Working { get; set; }

        //TODO
        //public float? geo_lat { get; set; }
        //public float? geo_long { get; set; }
    }
}
