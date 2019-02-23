using dTax.Auth;
using dTax.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CabRideController : Controller
    {
        private DbPostrgreContext db;

        public CabRideController(DbPostrgreContext context)
        {
            db = context;
        }

        //TODO 
        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [Authorize]
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> RideList()
        {
            //IEnumerable<CabRide> rides = db.CabRides.ToList();
            return Json(db.CabRides.ToList());
        }
    }
}
