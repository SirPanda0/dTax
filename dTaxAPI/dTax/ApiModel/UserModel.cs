﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.ApiModel
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
