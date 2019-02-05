using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dTax.Models;
using Microsoft.AspNetCore.Authorization;
using dTax.ApiModel;
using System.Text;



namespace dTax.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversController : Controller
    {

        private DbPostrgreContext db;

        public DriversController(DbPostrgreContext context)
        {
            db = context;
        }


        [Authorize]
        [Route("DriveReg")]
        [HttpPost]
        public async Task<IActionResult> DriverReg([FromBody] DriverRegistration registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные");
                }

                User user = await db.Users
                        .FirstOrDefaultAsync(u => u.Id == registerModel.UserId && u.IsDriver == true);

                Driver driver = await db.Drivers
                    .FirstOrDefaultAsync(dr => dr.UserId == registerModel.UserId || dr.DrivingLicence == registerModel.DrivingLicence);

                if (user != null && driver == null)
                {
                    Driver regd = new Driver
                    {
                        UserId = registerModel.UserId,
                        DrivingLicence = registerModel.DrivingLicence,
                        ExpiryDate = registerModel.ExpiryDate,
                        Working = false
                    };

                    await db.Drivers.AddAsync(regd);
                    await db.SaveChangesAsync();

                    
                    return Json(regd);

                }

                return BadRequest("Проверьте данные!");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}