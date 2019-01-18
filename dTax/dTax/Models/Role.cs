using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dTax.Models
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
