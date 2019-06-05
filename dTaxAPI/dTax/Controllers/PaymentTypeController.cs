using dTax.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class PaymentTypeController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public PaymentTypeController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var list = DBWorkflow.PaymentTypeRepository.GetPaymentTypes();

                return Json(list);
            }
            catch (Exception e)
            {
                Log.Error("\nErrorMassage: \n{0}\nStackTrace: \n{1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

    }
}
