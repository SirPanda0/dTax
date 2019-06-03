using dTax.ApiModel;
using dTax.Auth;
using dTax.Data.Interfaces;
using dTax.Entity.Models.Cabs;
using dTax.Entity.Models.CarModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class CabController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public CabController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Get")]
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                Guid DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                CabEntity cab = DBWorkflow.CabRepository.GetCabByDriverId(DriverId);

                if (cab != null)
                {
                    CabView cabView = new CabView()
                    {
                        Id = cab.Id,
                        CarBrandId = cab.CarBrandId,
                        CarBrand = cab.CarBrand.Name,
                        CarModelId = cab.CarModelId,
                        CarModel = cab.CarModel.Name,
                        CarColorId = cab.CarColorId,
                        CarColor = cab.CarColor.Name,
                        CarTypeId = cab.CarTypeId,
                        CarType = cab.CarType.Name,
                        DriverId = cab.DriverId,
                        LicensePlate = cab.LicensePlate,
                        ManufactureYear = cab.ManufactureYear,
                        VIN = cab.ManufactureYear.ToString()
                    };
                    return Json(cabView);
                }
                return BadRequest("У водителя не добавлен автомобиль");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Update")]
        [HttpPut]
        public ActionResult Update([FromBody] CabRegistrationModel updateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                Guid DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                CabEntity cab = DBWorkflow.CabRepository.GetCabByDriverId(DriverId);
                if (cab != null)
                {
                    cab.LicensePlate = updateModel.LicensePlate;
                    cab.VIN = updateModel.VIN;
                    cab.CarModelId = updateModel.CarModelId;
                    cab.ManufactureYear = updateModel.ManufactureYear;
                    cab.DriverId = DriverId;
                    cab.CarBrandId = updateModel.CarBrandId;
                    cab.CarTypeId = updateModel.CarTypeId;
                    cab.CarColorId = updateModel.CarColorId;

                    DBWorkflow.CabRepository.Insert(cab);
                    DBWorkflow.CabRepository.Commit();

                    var driver = DBWorkflow.DriverRepository.IsUnConfirmed(DriverId);
                    DBWorkflow.UserRepository.IsHalfReg(driver.UserId);
                }
                return BadRequest("У водителя не добавлен автомобиль");

            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Delete")]
        [HttpDelete]
        public ActionResult Delete()
        {
            try
            {
                Guid DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                CabEntity cab = DBWorkflow.CabRepository.GetCabByDriverId(DriverId);
                if (cab != null)
                {
                    cab.IsDeleted = true;
                    DBWorkflow.CabRepository.Update(cab);
                    DBWorkflow.CabRepository.Commit();

                    var driver = DBWorkflow.DriverRepository.IsUnConfirmed(DriverId);
                    DBWorkflow.UserRepository.IsHalfReg(driver.UserId);

                    return StatusCode(200);
                }
                return BadRequest("Нет существующих автомобилей");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }



        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("DriverCab")]
        [HttpGet]
        public ActionResult GetFullCabModel(Guid DriverId)
        {
            try
            {
                CabEntity cab = DBWorkflow.CabRepository.GetCabByDriverId(DriverId);
                if (cab != null)
                {
                    CabView cabView = new CabView()
                    {
                        Id = cab.Id,
                        CarBrandId = cab.CarBrandId,
                        CarBrand = cab.CarBrand.Name,
                        CarModelId = cab.CarModelId,
                        CarModel = cab.CarModel.Name,
                        CarColorId = cab.CarColorId,
                        CarColor = cab.CarColor.Name,
                        CarTypeId = cab.CarTypeId,
                        CarType = cab.CarType.Name,
                        DriverId = cab.DriverId,
                        LicensePlate = cab.LicensePlate,
                        ManufactureYear = cab.ManufactureYear,
                        VIN = cab.ManufactureYear.ToString()
                    };
                    return Json(cabView);
                }
                return Json("У водителя не добавлен автомобиль");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("Add")]
        [HttpPost]
        public ActionResult AddCab([FromBody] CabRegistrationModel registerModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                bool exist = DBWorkflow.CabRepository.IsExists(registerModel.LicensePlate, registerModel.VIN);

                if (exist)
                {
                    return BadRequest("Проверьте данные!");
                }

                Guid DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                CabEntity cab = new CabEntity()
                {
                    LicensePlate = registerModel.LicensePlate,
                    VIN = registerModel.VIN,
                    CarModelId = registerModel.CarModelId,
                    ManufactureYear = registerModel.ManufactureYear,
                    DriverId = DriverId,
                    CarBrandId = registerModel.CarBrandId,
                    CarTypeId = registerModel.CarTypeId,
                    CarColorId = registerModel.CarColorId

                };
                DBWorkflow.CabRepository.Insert(cab);
                DBWorkflow.CabRepository.Commit();

                return Ok(cab.Id);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("FileToCab")]
        [HttpGet]
        public ActionResult AddFile(Guid FileId)
        {
            try
            {
                Guid DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                Guid CabId = DBWorkflow.CabRepository.GetCabByDriverId(DriverId).Id;

                if (DBWorkflow.CabFileRepository.Exist(FileId) != false)
                {
                    return BadRequest();
                }

                DBWorkflow.CabFileRepository.AddLinkCab(CabId, FileId);

                return StatusCode(200);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }

        }

    }
}
