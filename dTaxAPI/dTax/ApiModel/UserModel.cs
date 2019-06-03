using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class UserModel
    {
        //public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
