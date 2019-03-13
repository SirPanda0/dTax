using dTax.ApiModel;
using dTax.Auth;
using dTax.Enums;
using dTax.Interfaces;
using dTax.Models;
using dTax.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class CabRideController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public CabRideController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }

        //TODO 
        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Authorize]
        [Route("GetList")]
        [HttpGet]
        public IActionResult RideList(int page, int size = 20)
        {

            var list = DBWorkflow.CabRideRepository.GetCabRideList();
            return Json(GetPagingCollections(list, page, size));

        }

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Authorize]
        [Route("GetStatus")]
        [HttpPost]
        public async Task<IActionResult> GetRideStatus(Guid id)
        {
            var ride = await GetStatus(id);
            return Ok(ride.StatusId);

        }


        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Authorize]
        [Route("Add")]
        [HttpPost]
        public IActionResult AddRide([FromBody] Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }

                Guid id = DBWorkflow.CustomerRepository.GetCustomerByUserId(GetUserIdByContext());

                bool ActiveBookExist = DBWorkflow.CabRideRepository.ActiveBookExist(id);
                if (ActiveBookExist)
                {
                    return BadRequest("У вас есть незаконченные поездки!");
                }

                decimal Price = CalculateBookPrice(booking.Distance);

                CabRide ride = new CabRide()
                {
                    CustomerId = id,
                    AddressStartPoint = booking.AddressStartPoint,
                    AddressEndPoint = booking.AddressEndPoint,
                    StartPointGPS = booking.StartPointGPS,
                    EndPointGPS = booking.EndPointGPS,
                    PaymentTypeId = booking.PaymentTypeId,
                    BookDetails = booking.BookDetails,
                    Price = Price
                };
                DBWorkflow.CabRideRepository.Insert(ride);
                

                CabRideStatus rideStatus = new CabRideStatus()
                {
                    CabRideId =  ride.Id,
                    StatusId = (int)RideStatusEnum.New,
                    StatusTime = DateTime.Now
                };

                DBWorkflow.CabRideStatusRepository.Insert(rideStatus);

                DBWorkflow.CabRideRepository.Commit();
                DBWorkflow.CabRideStatusRepository.Commit();

                return Ok(ride);

            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Authorize]
        [Route("Cancel")]
        [HttpPost]
        public IActionResult CancelRide(Guid id)
        {
            try
            {

                CabRide ride = DBWorkflow.CabRideRepository.GetCabRideById(id);

                ride.Canceled = true;

                CabRideStatus rideStatus = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                rideStatus.StatusId = (int)RideStatusEnum.Canceled;

                DBWorkflow.CabRideRepository.UpdateEntity(ride);
                DBWorkflow.CabRideStatusRepository.UpdateEntity(rideStatus);

                return Ok("Поездка отменена успешно!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        #region Private Region
        private IEnumerable<CabRideViewModel> GetRideModelList(IEnumerable<CabRide> rides)
        {
            return rides.Where(_ => DBWorkflow.CabRepository.GetCabByDriverId(_.Id) != null).Select(_ =>

            {
                var ridelist = new CabRideViewModel()
                {
                    Id = _.Id,
                    AddressStartPoint = _.AddressStartPoint,
                    AddressEndPoint = _.AddressEndPoint,
                    Price = _.Price
                };
                return ridelist;
            }).ToList();
        }

        private async Task<CabRideStatus> GetStatus (Guid id)
        {
            return await DBWorkflow.CabRideStatusRepository.GetQuery().FirstOrDefaultAsync(_ => _.CabRideId == id);
        }
        #endregion


    }
}
