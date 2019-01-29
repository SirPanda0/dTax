using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class Driver
    {
        
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DrivingLicence { get; set; } //номер прав
        public DateTime ExpiryDate { get; set; } //дата окончания действия прав
        public bool Working { get; set; }
    }
}
