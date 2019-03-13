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
using dTax.Models.Many;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : BaseUtilsController
    {

        private IDriverRepository driverRepository;
        private ICabRepository cabRepository;
        private ICarModelsRepository carModelsRepository;
        private IFileStorageRepository fileStorageRepository;
        private IDriverFileRepository driverFileRepository;
        public DriversController(
            IDriverRepository injectedriverRepository,
             ICabRepository injectedcabRepository,
             ICarModelsRepository injectedcarModelsRepository,
             IFileStorageRepository injectedfileStorageRepository,
             IDriverFileRepository injectedriverFileRepository
            )
        {
            driverRepository = injectedriverRepository;
            cabRepository = injectedcabRepository;
            carModelsRepository = injectedcarModelsRepository;
            fileStorageRepository = injectedfileStorageRepository;
            driverFileRepository = injectedriverFileRepository;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Register")]
        [HttpPost]
        public ActionResult DriverReg([FromBody] DriverRegistration registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                

                Guid id = GetUserIdByContext();

                bool exist = driverRepository.IsExists(id, registerModel.DrivingLicence,
                    registerModel.PassportSerial, registerModel.PassportNumber);

                if (exist)
                {
                    return BadRequest("Проверьте данные!");
                }

                Driver driver = new Driver()
                {
                    UserId = id,
                    DrivingLicence = registerModel.DrivingLicence,
                    ExpiryDate = registerModel.ExpiryDate,
                    PassportSerial = registerModel.PassportSerial,
                    PassportNumber = registerModel.PassportNumber
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
        [Route("FileToDriver")]
        [HttpPost]
        public ActionResult AddFile(Guid DriverId, Guid FileId)
        {
           driverFileRepository.AddLinkDriver(DriverId, FileId);
           return Ok();
        }



        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("TakeOrder")]
        [HttpPost]
        public ActionResult TakeOrder([FromBody] Guid id )
        {




            return Ok("WIP");
        }
    }
}