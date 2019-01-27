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
        //or
        //public string Login { get; set; }
        //public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int DrivingLicence { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Working { get; set; }
    }
}
