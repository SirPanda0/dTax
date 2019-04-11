using dTax.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class CarTypeController : BaseUtilsController
    {
        private ICarTypeRepository carTypeRepository;
        public CarTypeController(ICarTypeRepository injectedcarTypeRepository)
        {
            carTypeRepository = injectedcarTypeRepository;
        }
        //[PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var list = carTypeRepository.GetListTypes();
            return Json(list);
        }
    }
}
