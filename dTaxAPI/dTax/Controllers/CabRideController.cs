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

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetActiveCustomerRide")]
        [HttpGet]
        public IActionResult GetCustomerRide()
        {
            try
            {
                var CustomerId = DBWorkflow.CustomerRepository.GetCustomerByUserId(GetUserIdByContext());

                var ride = DBWorkflow.CabRideRepository.GetCabRideByCustomerId(CustomerId);

                string tariff = "";

                switch (ride.TariffType)
                {
                    case (int)TariffTypeEnum.Standart:
                        {
                            tariff = "Стандарт";
                            break;
                        };
                    case (int)TariffTypeEnum.Comfort:
                        {
                            tariff = "Комфорт";
                            break;
                        };
                    case (int)TariffTypeEnum.Emergency:
                        {
                            tariff = "Экстренный";
                            break;
                        };
                }

                FullCabRideCustomerViewModel fullCab = new FullCabRideCustomerViewModel()
                {
                    Id = ride.Id,
                    AddressStartPoint = ride.AddressStartPoint,
                    AddressEndPoint = ride.AddressEndPoint,
                    Price = ride.Price,
                    BookDetails = ride.BookDetails,
                    PaymentType = ride.PaymentType.TypeName,
                    TariffType = tariff,
                    RideStatus = ride.CabRideStatusLink.FirstOrDefault().Status.StatusName,
                    IsAssigned = false,
                    PaymentTypeId = ride.PaymentTypeId,
                    TariffTypeId = ride.TariffType,
                    RideStatusId =ride.CabRideStatusLink.FirstOrDefault().StatusId
                };

                var shift = ride.CabRideStatusLink.FirstOrDefault().Shift;

                if (shift != null)
                {
                    var cab = DBWorkflow.CabRepository.GetCabById(shift.CabId);
                    fullCab.IsAssigned = true;
                    fullCab.LicensePlate = cab.LicensePlate;
                    fullCab.CarBrand = cab.CarBrand.Name;
                    fullCab.CarModel = cab.CarModel.Name;
                    fullCab.CarColor = cab.CarColor.Name;
                }

                return Json(fullCab);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetActiveDriverRide")]
        [HttpGet]
        public IActionResult GetActiveDriverRide()
        {
            try
            {

                var DriverId = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext()).Id;

                var ride = DBWorkflow.CabRideRepository.GetCabRideByDriverId(DriverId);

                string tariff = "";

                switch (ride.TariffType)
                {
                    case (int)TariffTypeEnum.Standart:
                        {
                            tariff = "Стандарт";
                            break;
                        };
                    case (int)TariffTypeEnum.Comfort:
                        {
                            tariff = "Комфорт";
                            break;
                        };
                    case (int)TariffTypeEnum.Emergency:
                        {
                            tariff = "Экстренный";
                            break;
                        };
                }

                FullDriverCabRideViewModel fullDriver = new FullDriverCabRideViewModel()
                {
                    Id = ride.Id,
                    AddressStartPoint = ride.AddressStartPoint,
                    AddressEndPoint = ride.AddressEndPoint,
                    Price = ride.Price,
                    BookDetails = ride.BookDetails,
                    PaymentType = ride.PaymentType.TypeName,
                    TariffType = tariff,
                    RideStatus = ride.CabRideStatusLink.FirstOrDefault().Status.StatusName,
                    PaymentTypeId = ride.PaymentTypeId,
                    TariffTypeId = ride.TariffType,
                    RideStatusId = ride.CabRideStatusLink.FirstOrDefault().StatusId
                };

                return Json(fullDriver);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        //TODO 
        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetRideList")]
        [HttpGet]
        public IActionResult GetRideList(int page, int size = 20)
        {

            var list = DBWorkflow.CabRideRepository.GetCabRideList(page, size);
            return Json(GetRideModelList(list));

        }

        //[PolicyAuthorize(AuthorizePolicy.Driver)]
        //[PolicyAuthorize(AuthorizePolicy.FullAccess)]
        //[Route("ActiveOrder")]
        //[HttpGet]
        //public ActionResult ActiveOrder(int page, int size = 20)
        //{
        //    try
        //    {
        //        var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

        //        var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

        //        var list = DBWorkflow.CabRideRepository.GetCabRideList(page, size);
        //        return Json(GetPagingCollections(GetRideDriverModelList(list, shift.Id), page, size));

        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
        //        return StatusCode(500);
        //    }
        //}

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetRideStatus")]
        [HttpGet]
        public async Task<IActionResult> GetRideStatus()
        {
            var id = DBWorkflow.CustomerRepository.GetCustomerByUserId(GetUserIdByContext());
            var ride = await GetStatus(id);
            return Ok(ride.StatusId);
        }

        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("GetRidePrice")]
        [HttpGet]
        public IActionResult GetRidePrice(double distance)
        {
            int dis = Convert.ToInt32(distance);
            PriceView Price = CalculateBookPrice(dis / 1000);
            return Ok(Price);

        }


        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("AddOrder")]
        [HttpPost]
        public IActionResult AddRide([FromBody] BookingModel booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные!");
                }


                Guid CustomerId = DBWorkflow.CustomerRepository.GetCustomerByUserId(GetUserIdByContext());

                bool ActiveBookExist = DBWorkflow.CabRideRepository.ActiveBookExist(CustomerId);
                if (ActiveBookExist)
                {
                    return BadRequest("У вас есть незаконченные поездки!");
                }

                int dis = Convert.ToInt32(booking.Distance);

                PriceView PriceResponse = CalculateBookPrice(dis / 1000);

                var Price = PriceResponse.Standart;

                switch (booking.TariffType)
                {
                    case (int)TariffTypeEnum.Standart:
                        {
                            Price = PriceResponse.Standart;
                            break;
                        }
                    case (int)TariffTypeEnum.Comfort:
                        {
                            Price = PriceResponse.Comfort;
                            break;
                        }
                    case (int)TariffTypeEnum.Emergency:
                        {
                            Price = PriceResponse.Emergency;
                            break;
                        }
                };

                CabRideEntity ride = new CabRideEntity()
                {
                    CustomerId = CustomerId,
                    AddressStartPoint = booking.AddressStartPoint,
                    AddressEndPoint = booking.AddressEndPoint,
                    StartPointGPS = booking.StartPointGPS,
                    EndPointGPS = booking.EndPointGPS,
                    PaymentTypeId = booking.PaymentTypeId,
                    BookDetails = booking.BookDetails,
                    Price = Price,
                    TariffType = booking.TariffType
                };
                DBWorkflow.CabRideRepository.Insert(ride);


                CabRideStatusEntity rideStatus = new CabRideStatusEntity()
                {
                    CabRideId = ride.Id,
                    StatusId = GetStatusId((int)RideStatusEnum.New),
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
        [HttpDelete]
        public IActionResult CancelRide(Guid id)
        {
            try
            {

                CabRideEntity ride = DBWorkflow.CabRideRepository.GetCabRideById(id);

                ride.IsCanceled = true;

                CabRideStatusEntity rideStatus = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                rideStatus.StatusId = GetStatusId((int)RideStatusEnum.Canceled);
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


        [PolicyAuthorize(AuthorizePolicy.User)]
        [PolicyAuthorize(AuthorizePolicy.FullAccess)]
        [Route("UpdateOrder")]
        [HttpPut]
        public IActionResult UpdateRide(OrderUpdateModel order)
        {
            try
            {

                CabRideEntity ride = DBWorkflow.CabRideRepository.GetCabRideById(order.Id);

                ride.AddressStartPoint = order.AddressStartPoint;
                ride.AddressEndPoint = order.AddressEndPoint;
                ride.StartPointGPS = order.StartPointGPS;
                ride.EndPointGPS = order.EndPointGPS;
                ride.TariffType = order.TariffType;
                ride.PaymentTypeId = order.PaymentTypeId;

                DBWorkflow.CabRideRepository.UpdateEntity(ride);

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
        [HttpGet]
        public ActionResult TakeOrder(Guid id)
        {
            try
            {
                var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                //RideId не статуса а поездки
                var ride = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                var check = DBWorkflow.CabRideStatusRepository.GetCabRideList().Where(_ => _.ShiftId == shift.Id && _.StatusId != GetStatusId((int)RideStatusEnum.Ended)).Count();

                if (check != 0)
                {
                    return BadRequest("У вас есть незаконченные поездки!");
                }

                ride.StatusId = GetStatusId((int)RideStatusEnum.Assigned);
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
        [HttpGet]
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

                ride.StatusId = GetStatusId((int)RideStatusEnum.Started);

                ride.RideStartTime = DateTime.Now;

                ride.StatusTime = DateTime.Now;

                DBWorkflow.CabRideStatusRepository.UpdateEntity(ride);

                return Json("Поездка начата!");
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
        [HttpGet]
        public ActionResult EndRide(Guid id)
        {
            try
            {
                var re = DBWorkflow.CabRideRepository.GetCabRideById(id);
                //RideId не статуса а поездки
                var ride = DBWorkflow.CabRideStatusRepository.GetCabRideStatusByRideId(id);

                var driver = DBWorkflow.DriverRepository.GetDriverByUserId(GetUserIdByContext());

                var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                if (ride.ShiftId != shift.Id)
                {
                    return BadRequest();
                }

                re.IsDeleted = true;

                ride.StatusId = GetStatusId((int)RideStatusEnum.Ended);

                ride.RideEndTime = DateTime.Now;
                ride.StatusTime = DateTime.Now;

                DBWorkflow.CabRideStatusRepository.UpdateEntity(ride);
                DBWorkflow.CabRideRepository.UpdateEntity(re);

                return Json("Поездка завершена!");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        #region Private Region

        private Guid GetStatusId(int number)
        {
            try
            {
                var status = DBWorkflow.StatusesRepository.GetStatuses();

                switch (number)
                {
                    case (int)RideStatusEnum.New:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 1).Id;
                        }
                    case (int)RideStatusEnum.Assigned:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 2).Id;
                        }
                    case (int)RideStatusEnum.Started:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 3).Id;
                        }
                    case (int)RideStatusEnum.Ended:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 4).Id;
                        }
                    case (int)RideStatusEnum.Canceled:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 5).Id;
                        }
                    case (int)RideStatusEnum.Arrived:
                        {
                            return status.FirstOrDefault(n => n.StatusNumber == 6).Id;
                        }
                }
                return Guid.Empty;
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return Guid.Empty;
            }
        }


        private IEnumerable<CabRideViewModel> GetRideModelList(IEnumerable<CabRideEntity> rides)
        {
            return rides
                
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

        private IEnumerable<CabRideViewModel> GetRideDriverModelList(IEnumerable<CabRideEntity> rides, Guid id)
        {

            return rides
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

        private async Task<CabRideStatusEntity> GetStatus(Guid id)
        {
            return await DBWorkflow.CabRideStatusRepository.GetQuery().FirstOrDefaultAsync(_ => _.CabRideId == id);
        }
        #endregion


    }
}
