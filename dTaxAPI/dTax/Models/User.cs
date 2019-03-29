using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Models
{
    public class User : BaseEntity
    {
        
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime LastLogin { get; set; } = DateTime.Now;

        //Булевые 1- о полной регистрации, 2- удален ли аккаунт
        public bool FullReg { get; set; } = false;
        
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }

    }
}
