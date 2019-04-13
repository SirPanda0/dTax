﻿using dTax.ApiModel;
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
        [Route("Add")]
        [HttpPost]
        public ActionResult AddCab([FromBody] CabRegistration registerModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                //bool ExistFile = DBWorkflow.FileStorageRepository.IsExists(registerModel.FileStorageId);
                //if (!ExistFile)
                //{
                //    return BadRequest("Проверьте прикрепленные файлы!");
                //}

                bool exist = DBWorkflow.CabRepository.IsExists(registerModel.LicensePlate, registerModel.VIN);

                if (exist)
                {
                   return BadRequest("Проверьте данные!");
                }

                Cab cab = new Cab()
                {
                    LicensePlate = registerModel.LicensePlate,
                    VIN = registerModel.VIN,
                    CarModelId = registerModel.CarModelId,
                    ManufactureYear = registerModel.ManufactureYear,
                    DriverId = registerModel.DriverId,
                    CarBrandId = registerModel.CarBrandId,
                    CarTypeId = registerModel.CarTypeId,
                    CarColorId = registerModel.CarColorId

                };
                DBWorkflow.CabRepository.Insert(cab);
                DBWorkflow.CabRepository.Commit();

                return Ok("Загрузите фотографии!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [Route("FileToCab")]
        [HttpPost]
        public ActionResult AddFile(Guid CabId, Guid FileId)
        {
            DBWorkflow.CabFileRepository.AddLinkCab(CabId, FileId);
            
            return Ok();
        }

    }
}
