using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Models;
using Newtonsoft.Json;

namespace dTax.ApiModel
{
    public class RegisterModel 
    {

        public string Login { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public bool IsDriver { get; set; }
        


    }
}
