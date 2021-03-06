﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Auth;
using dTax.Models;
using Microsoft.AspNetCore.Mvc;

namespace dTax.Controllers
{


    
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        DbPostrgreContext PostrgreContext;

        public ValuesController(DbPostrgreContext postrgreContext)
        {
            PostrgreContext = postrgreContext;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
