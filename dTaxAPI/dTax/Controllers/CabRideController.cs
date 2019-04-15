using dTax.ApiModel;
using dTax.Auth;
using dTax.Common.Enums;
using dTax.Data.Interfaces;
using dTax.Entity.Models.CabRides;
using dTax.Entity.Models.CabRideStatuses;

 
using dTax.ResponseModels;
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
        [Route("GetRideList")]
        [HttpGet]
        public IActionResult RideList(int page, int size = 20)
        {

            var list = DBWorkflow.CabRideRepository.GetCabRideList();
            return Json(GetPagingCollections(GetRideModelList(list), page, size));

        }

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetRideStatus")]
        [HttpPost]
        public async Task<IActionResult> GetRideStatus(Guid id)
        {
            var ride = await GetStatus(id);
            return Ok(ride.StatusId);

        }

        //[PolicyAuthorize(AuthorizePolicy.User)]
        //[PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetRidePrice")]
        [HttpGet]
        public IActionResult GetRidePrice(double distance)
        {
            int dis = Convert.ToInt32(distance);
            decimal Price = CalculateBookPrice(dis / 1000);
            return Ok(Price);

        }


        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("AddOrder")]
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
                    CabRideId = ride.Id,
                    StatusId = (int)RideStatusEnum.New,
                    StatusTime = DateTime.Now
                };

                DBWorkflow.CabRideStatusRepository.Insert(rideStatus);

                DBWorkflow.CabRideRepository.Commit();
                DBWorkflow.CabRideStatusRepository.Commit();

                RideResponse response = new RideResponse
                {
                    Id = ride.Id,
                    AddressStartPoint = ride.AddressStartPoint,
                    AddressEndPoint = ride.AddressEndPoint,
                    StartPointGPS = ride.StartPointGPS,
                    EndPointGPS = ride.EndPointGPS,
                    PaymentTypeId = ride.PaymentTypeId,
                    Price = ride.Price,
                    BookDetails = ride.BookDetails
                };


                return Ok(response);

            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        //[PolicyAuthorize(AuthorizePolicy.User)]
        //[PolicyAuthorize(AuthorizePolicy.FullAccess)]
        //[Authorize]
        //[Route("EditOrder")]
        //[HttpPost]
        //public IActionResult EditOrder([FromBody] Booking booking)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest("Проверьте данные!");
        //        }

        //        Guid id = DBWorkflow.CustomerRepository.GetCustomerByUserId(GetUserIdByContext());

        //        decimal Price = CalculateBookPrice(booking.Distance);

        //        DBWorkflow.CabRideRepository.GetCabRideById();

        //        CabRide ride = new CabRide()
        //        {
        //            CustomerId = id,
        //            AddressStartPoint = booking.AddressStartPoint,
        //            AddressEndPoint = booking.AddressEndPoint,
        //            StartPointGPS = booking.StartPointGPS,
        //            EndPointGPS = booking.EndPointGPS,
        //            PaymentTypeId = booking.PaymentTypeId,
        //            BookDetails = booking.BookDetails,
        //            Price = Price
        //        };
        //        DBWorkflow.CabRideRepository.UpdateEntity(ride);


        //        CabRideStatus rideStatus = new CabRideStatus()
        //        {
        //            CabRideId = ride.Id,
        //            StatusId = (int)RideStatusEnum.New,
        //            StatusTime = DateTime.Now
        //        };

        //        DBWorkflow.CabRideStatusRepository.Insert(rideStatus);

        //        DBWorkflow.CabRideRepository.Commit();
        //        DBWorkflow.CabRideStatusRepository.Commit();

        //        RideResponse response = new RideResponse
        //        {
        //            Id = ride.Id,
        //            AddressStartPoint = ride.AddressStartPoint,
        //            AddressEndPoint = ride.AddressEndPoint,
        //            StartPointGPS = ride.StartPointGPS,
        //            EndPointGPS = ride.EndPointGPS,
        //            PaymentTypeId = ride.PaymentTypeId,
        //            Price = ride.Price,
        //            BookDetails = ride.BookDetails
        //        };


        //        return Ok(response);

        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
        //        return StatusCode(500);
        //    }
        //}

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("CancelOrder")]
        [HttpPost]
        public IActionResult CancelRide(Guid id)
        {
            try
            {

                CabRide ride = DBWorkflow.CabRideRepository.GetCabRideById(id);

                ride.IsCanceled = true;

                CabRideStatus rideStatus = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                rideStatus.StatusId = (int)RideStatusEnum.Canceled;
                rideStatus.StatusTime = DateTime.Now;

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

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("TakeOrder")]
        [HttpPost]
        public ActionResult TakeOrder(Guid id)
        {
            try
            {
                var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                //RideId не статуса а поездки
                var ride = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                var check = DBWorkflow.CabRideStatusRepository.GetCabRideList().Where(_=>_.ShiftId == shift.Id).Count();

                if (check != 0)
                {
                    return BadRequest("У вас есть незаконченные поездки!");
                }

                ride.StatusId = (int)RideStatusEnum.Assigned;
                ride.StatusTime = DateTime.Now;

                ride.ShiftId = DBWorkflow.ShiftRepository.GetShiftByDriverId(DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id).Id;

                DBWorkflow.CabRideStatusRepository.UpdateEntity(ride);

                return Ok("Поездка взята!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("StartRide")]
        [HttpPost]
        public ActionResult StartRide(Guid id)
        {
            try
            {
                //RideId не статуса а поездки
                var ride = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                if (ride.ShiftId != shift.Id)
                {
                    return BadRequest();
                }

                ride.StatusId = (int)RideStatusEnum.Started;

                ride.RideStartTime = DateTime.Now;

                ride.StatusTime = DateTime.Now;

                DBWorkflow.CabRideStatusRepository.UpdateEntity(ride);

                return Ok("Поездка начата!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("EndRide")]
        [HttpPost]
        public ActionResult EndRide(Guid id)
        {
            try
            {
                //RideId не статуса а поездки
                var ride = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                if (ride.ShiftId != shift.Id)
                {
                    return BadRequest();
                }

                ride.StatusId = (int)RideStatusEnum.Ended;

                ride.RideEndTime = DateTime.Now;
                ride.StatusTime = DateTime.Now;

                DBWorkflow.CabRideStatusRepository.UpdateEntity(ride);

                return Ok("Поездка завершена!");
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
            return rides.Where(_ => DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(_.Id).StatusId == (int)RideStatusEnum.New)
                .Select(_ =>

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

        private async Task<CabRideStatus> GetStatus(Guid id)
        {
            return await DBWorkflow.CabRideStatusRepository.GetQuery().FirstOrDefaultAsync(_ => _.CabRideId == id);
        }
        #endregion


    }
}
