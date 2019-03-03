using dTax.Auth;
using dTax.Interfaces;
using dTax.Models;
using dTax.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("[controller]")]
    [ApiController]
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




        #region Private Region
        private IEnumerable<CabRideViewModel> GetStatementModelList(IEnumerable<CabRide> rides)
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
        #endregion


    }
}
