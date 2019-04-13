using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
 
using Newtonsoft.Json;

namespace dTax.ApiModel
{
    public class RegisterModel 
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public int RoleId { get; set; }

    }
}
