﻿using dTax.Auth;
using dTax.Data.Interfaces;
using dTax.Entity.Models.Drivers;
using dTax.Entity.Models.Shifts;
using dTax.ResponseModels;
using dTax.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class OperatorController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public OperatorController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }

        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [HttpGet]
        [Route("GetList")]
        public IActionResult GetListAsync(int page, int size = 20)
        {
            try
            {
                var list = GetDriversModelList(DBWorkflow.DriverRepository.GetListDrivers());
                return Json(GetPagingCollections(list, page, size));

            }
            catch (Exception e)
            {
                Log.Error("\nErrorMassage: \n{0}\nStackTrace: \n{1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }

        }

        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [HttpGet]
        [Route("GetUnconfirmedList")]
        public IActionResult GetUnconfirmedList(int page, int size = 20)
        {
            try
            {
                var list = GetUnconfirmedDriversModelList(DBWorkflow.DriverRepository.GetUnconfirmedDrivers());
                return Json(GetPagingCollections(list, page, size));

            }
            catch (Exception e)
            {
                Log.Error("\nErrorMassage: \n{0}\nStackTrace: \n{1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }

        }

        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [HttpGet]
        [Route("GetInfo")]
        public IActionResult GetInfo(Guid DriverId)
        {
            try
            {
                var driver = DBWorkflow.DriverRepository.GetDriverById(DriverId);

                var cab = DBWorkflow.CabRepository.GetCabByDriverId(DriverId);

                var cabFiles = cab.FileLink.Where(_ => _.IsDeleted != true).Select(f => f.FileId).ToArray();
                var driverFiles = driver.FileLink.Where(_ => _.IsDeleted != true).Select(f => f.FileId).ToArray();

                DriverResponse driverResponse = new DriverResponse()
                {
                    Id = DriverId,
                    Name = DBWorkflow.UserRepository.GetUserFioById(driver.UserId),
                    DrivingLicence = driver.DrivingLicence,
                    ExpiryDate = driver.ExpiryDate,
                    RegistrationDate = driver.RegistrationDate,
                    PassportSerial = driver.PassportSerial,
                    PassportNumber = driver.PassportNumber,
                    DriverFilesIds = driverFiles,
                    CabFilesIds = cabFiles
                    // TODO DriverFileStorageId = driver.FileLink,
                    //Cab = cab,
                    //TODO
                    //CarModel = DBWorkflow.CarModelsRepository.GetCarModelById(cab.CarModelId)
                };

                return Ok(driverResponse);
            }
            catch (Exception e)
            {
                Log.Error("\nErrorMassage: \n{0}\nStackTrace: \n{1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }


        [PolicyAuthorize(AuthorizePolicy.Operator)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [HttpPost]
        [Route("ConfirmDriver")]
        public IActionResult ConfirmDriver(Guid DriverId)
        {
            try
            {
                var driver = DBWorkflow.DriverRepository.IsConfirmed(DriverId);
                DBWorkflow.UserRepository.IsFullReg(driver.UserId);

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);
                var cab = DBWorkflow.CabRepository.GetCabByDriverId(driver.Id);

                if (shift == null)
                {
                    DBWorkflow.ShiftRepository.Insert(new Shift
                    {
                        DriverId = driver.Id,
                        CabId = cab.Id,
                        LoginTime = DateTime.Now
                    });
                }

                return Ok("Водитель подтвержден!");
            }
            catch (Exception e)
            {
                Log.Error("\nErrorMassage: \n{0}\nStackTrace: \n{1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        #region Private Region
        private IEnumerable<DriverViewModel> GetDriversModelList(IEnumerable<Driver> drivers)
        {
            return drivers.Where(_ => DBWorkflow.CabRepository.GetCabByDriverId(_.Id) != null && _.IsСonfirmed != false).Select(_ =>

              {
                  var driverlist = new DriverViewModel()
                  {
                      DriverId = _.Id,
                      FIO = DBWorkflow.UserRepository.GetUserFioById(_.UserId),
                      CreateDate = _.CreatedDate

                  };
                  return driverlist;
              }).ToList();
        }

        private IEnumerable<DriverViewModel> GetUnconfirmedDriversModelList(IEnumerable<Driver> drivers)
        {
            return drivers.Where(_ => DBWorkflow.CabRepository.GetCabByDriverId(_.Id) != null && _.IsСonfirmed != true).Select(_ =>

            {
                var driverlist = new DriverViewModel()
                {
                    DriverId = _.Id,
                    FIO = DBWorkflow.UserRepository.GetUserFioById(_.UserId),
                    CreateDate = _.CreatedDate

                };
                return driverlist;
            }).ToList();
        }
        #endregion

    }
}
