using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public bool IsDriver { get; set; }
        public bool FullReg { get; set; }

        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }

    }
}
