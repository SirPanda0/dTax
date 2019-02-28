using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dTax.Models;
using Serilog;
using dTax.ApiModel;
using System.Text;
using dTax.Auth;
using dTax.Interfaces.Repository;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : BaseUtilsController
    {

        private IDriverRepository driverRepository;
        private ICabRepository cabRepository;
        private ICarModelsRepository carModelsRepository;
        public DriversController(
            IDriverRepository injectedriverRepository,
             ICabRepository injectedcabRepository,
             ICarModelsRepository injectedcarModelsRepository
            )
        {
            driverRepository = injectedriverRepository;
            cabRepository = injectedcabRepository;
            carModelsRepository = injectedcarModelsRepository;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Register")]
        [HttpPost]
        public ActionResult DriverReg([FromBody] DriverRegistration registerModel)
        {
            try
            {
                if (registerModel.FileStorageId == Guid.Empty)
                {
                    return BadRequest("Файлы документов не прикреплены!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                Guid id = GetUserIdByContext();

                bool exist = driverRepository.IsExists(id, registerModel.DrivingLicence,
                    registerModel.PassportSerial, registerModel.PassportNumber);

                if (exist)
                {
                    BadRequest("Проверьте данные!");
                }

                Driver driver = new Driver()
                {
                    UserId = id,
                    DrivingLicence = registerModel.DrivingLicence,
                    ExpiryDate = registerModel.ExpiryDate,
                    PassportSerial = registerModel.PassportSerial,
                    PassportNumber = registerModel.PassportNumber,
                    FileStorageId = registerModel.FileStorageId
                };

                driverRepository.Insert(driver);
                driverRepository.Commit();

                return Ok(driver.Id);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }


        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("AddCab")]
        [HttpPost]
        public ActionResult AddCab([FromBody] CabRegistration registerModel)
        {
            try
            {
                if (registerModel.FileStorageId == Guid.Empty)
                {
                    return BadRequest("Файлы документов не прикреплены!");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                bool exist = cabRepository.IsExists(registerModel.LicensePlate, registerModel.VIN);

                if (exist)
                {
                    BadRequest("Проверьте данные!");
                }

                CarModel carModel = new CarModel()
                {
                    BrandName = registerModel.BrandName,
                    ModelName = registerModel.ModelName,
                    ModelType = registerModel.ModelType,
                    ModelColor = registerModel.ModelColor
                };

                carModelsRepository.Insert(carModel);
                carModelsRepository.Commit();

                Cab cab = new Cab()
                {
                    LicensePlate = registerModel.LicensePlate,
                    VIN = registerModel.VIN,
                    CarModelId = carModel.Id,
                    ManufactureYear = registerModel.ManufactureYear,
                    DriverId = registerModel.DriverId,
                    FileStorageId = registerModel.FileStorageId
                };
                cabRepository.Insert(cab);
                carModelsRepository.Commit();

                return Ok("Ожидайте проверки оператора!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        //[Authorize]
        //[Route("DriveReg")]
        //[HttpPost]
        //public async Task<IActionResult> DriverReg([FromBody] DriverRegistration registerModel)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Проверьте данные");
        //    }

        //    User user = await db.Users
        //            .FirstOrDefaultAsync(u => u.Id == registerModel.UserId );


        //    Driver driver = await db.Drivers
        //        .FirstOrDefaultAsync(dr => dr.UserId == registerModel.UserId
        //        || dr.DrivingLicence == registerModel.DrivingLicence);

        //    if (user != null && driver == null)
        //    {

        //        try
        //        {
        //            Driver regd = new Driver
        //            {
        //                UserId = registerModel.UserId,
        //                DrivingLicence = registerModel.DrivingLicence,
        //                ExpiryDate = registerModel.ExpiryDate,
        //                Working = false
        //            };

        //            await db.Drivers.AddAsync(regd);
        //            await db.SaveChangesAsync();

        //            Guid DriverId = regd.Id;

        //            CarModel car = new CarModel
        //            {
        //                BrandName = registerModel.BrandName,
        //                ModelName = registerModel.ModelName,
        //                ModelColor = registerModel.ModelDescription
        //            };

        //            await db.CarModels.AddAsync(car);
        //            await db.SaveChangesAsync();

        //            Cab cab = new Cab
        //            {
        //                LicensePlate = registerModel.LicensePlate,
        //                CarModelId = car.Id,
        //                VIN = registerModel.VIN,
        //                ManufactureYear = registerModel.ManufactureYear,
        //                DriverId = regd.Id,
        //                Active = true
        //            };

        //            await db.Cabs.AddAsync(cab);
        //            await db.SaveChangesAsync();

        //            Guid CabId = cab.Id;

        //            Shift shift = new Shift
        //            {
        //                DriverId = regd.Id,
        //                CabId = cab.Id,
        //                LoginTime = DateTime.Now
        //            };

        //            await db.Shifts.AddAsync(shift);

        //            user.FullReg = true;
        //            db.Users.Update(user);
        //            await db.SaveChangesAsync();

        //            return Json("Регистрация окончена!");
        //        }

        //        catch (Exception e)
        //        {
        //            user.FullReg = false;
        //            db.Users.Update(user);
        //            await db.SaveChangesAsync();

        //            return BadRequest("Не полная регистрация \n" + e);

        //        }


        //    }
        //    else
        //        return BadRequest("Проверьте данные!");

        //}


        //[Authorize]
        //[Route("TakeOrder")]
        //[HttpPost]
        //public async Task<IActionResult> TakeOrder([FromBody] )
        //{





        //}
    }
}