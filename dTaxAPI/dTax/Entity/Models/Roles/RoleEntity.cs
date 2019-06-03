using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Entity.Models.Base;
using dTax.Entity.Models.Users;
using Newtonsoft.Json;

namespace dTax.Entity.Models.Roles
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<UserEntity> Users { get; set; }

        public RoleEntity()
        {
            Users = new List<UserEntity>();
        }
    }
}
