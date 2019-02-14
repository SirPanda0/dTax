using dTax.Migrations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private DbPostrgreContext db;

        public CustomerController(DbPostrgreContext context)
        {
            db = context;

        }




    }
}
