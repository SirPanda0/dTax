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

            if (!ModelState.IsValid)
            {
                return BadRequest("Проверьте данные");
            }

            User user = await db.Users
                    .FirstOrDefaultAsync(u => u.Id == registerModel.UserId && u.IsDriver == true);


            Driver driver = await db.Drivers
                .FirstOrDefaultAsync(dr => dr.UserId == registerModel.UserId
                || dr.DrivingLicence == registerModel.DrivingLicence);

            if (user != null && driver == null)
            {

                try
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

                    Guid DriverId = regd.Id;

                    CarModel car = new CarModel
                    {
                        BrandName = registerModel.BrandName,
                        ModelName = registerModel.ModelName,
                        ModelDescription = registerModel.ModelDescription
                    };

                    await db.CarModels.AddAsync(car);
                    await db.SaveChangesAsync();

                    Cab cab = new Cab
                    {
                        LicensePlate = registerModel.LicensePlate,
                        CarModelId = car.Id,
                        VIN = registerModel.VIN,
                        ManufactureYear = registerModel.ManufactureYear,
                        DriverId = regd.Id,
                        Active = true
                    };

                    await db.Cabs.AddAsync(cab);
                    await db.SaveChangesAsync();

                    Guid CabId = cab.Id;

                    Shift shift = new Shift
                    {
                        DriverId = regd.Id,
                        CabId = cab.Id,
                        LoginTime = DateTime.Now
                    };

                    await db.Shifts.AddAsync(shift);

                    user.FullReg = true;
                    db.Users.Update(user);
                    await db.SaveChangesAsync();

                    return Json("Регистрация окончена!");
                }

                catch (Exception e)
                {
                    user.FullReg = false;
                    db.Users.Update(user);
                    await db.SaveChangesAsync();

                    return BadRequest("Не полная регистрация \n" + e);

                }


            }
            else
                return BadRequest("Проверьте данные!");

        }


        [Authorize]
        [Route("TakeOrder")]
        [HttpPost]
        public async Task<IActionResult> TakeOrder([FromBody] )
        {

            



        }
    }
}