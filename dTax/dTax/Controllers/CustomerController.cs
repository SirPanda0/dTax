using dTax.ApiModel;
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
    public class CustomerController : Controller
    {
        private DbPostrgreContext db;

        public CustomerController(DbPostrgreContext context)
        {
            db = context;
        }

        [Authorize]
        [HttpPost]
        [Route("CabBook")]
        public async Task<IActionResult> CabBook([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Проверьте данные");
            }

            CabRide ride = new CabRide
            {
                CustomerId = booking.CustomerId,
                AddressStartPoint = booking.AddressStartPoint,
                AddressEndPoint = booking.AddressEndPoint,
                PaymentTypeId = booking.PaymentTypeId,
                BookDetails = booking.BookDetails
            };

            await db.CabRides.AddAsync(ride);
            await db.SaveChangesAsync();

            return Json(ride);


        }
    }
}
