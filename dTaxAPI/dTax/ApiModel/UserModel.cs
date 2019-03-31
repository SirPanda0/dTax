using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity;

namespace dTax.ApiModel
{
    public class UserModel
    {
        
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}
