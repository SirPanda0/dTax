using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models.Users;
using Newtonsoft.Json;

namespace dTax.Entity.Models.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
