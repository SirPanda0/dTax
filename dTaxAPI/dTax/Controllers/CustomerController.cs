using dTax.ApiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dTax.Common;
using Microsoft.EntityFrameworkCore;
using dTax.Auth;
using Serilog;
using dTax.Entity;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private DbPostrgreContext db;

        public CustomerController(DbPostrgreContext context)
        {
            db = context;
        }

        
        
        //[HttpPost]
        //[Route("CabBook")]
        //public async Task<IActionResult> CabBook([FromBody] Booking booking)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Проверьте данные");
        //    }

        //    try
        //    {
        //        Customer customer = await db.Customers.FirstOrDefaultAsync(c => c.UserId == booking.UserId);

        //        //Можно ли создавать несколько заказов сразу?
        //        //CabRide ride = await db.CabRides.FirstOrDefaultAsync(r => r.CustomerId == customer.Id && r.Canceled == true);

        //        if (customer != null)
        //        {
        //            CabRide ride = new CabRide
        //            {
        //                CustomerId = customer.Id,
        //                AddressStartPoint = booking.AddressStartPoint,
        //                AddressEndPoint = booking.AddressEndPoint,
        //                PaymentTypeId = booking.PaymentTypeId,
        //                BookDetails = booking.BookDetails,
        //                Canceled = false,
        //                Price = GetPrice(booking.Distance)
        //            };

        //            await db.CabRides.AddAsync(ride);
        //            await db.SaveChangesAsync();

        //            return Json(ride);
        //        }
        //        return BadRequest("Вы не являетесь заказчиком!");

        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}


















        #region Private
        //TODO систему тарифов
        protected int GetPrice(int distance)
        {
            return distance * 8;
        }
        #endregion
    }
}
