using dTax.Auth;
using dTax.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class CarColorController : BaseUtilsController
    {
        private ICarColorRepository carColorRepository;
        public CarColorController(ICarColorRepository injectedcarColorRepository)
        {
            carColorRepository = injectedcarColorRepository;
        }
        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var list = carColorRepository.GetListColors();
            return Json(list);
        }
    }
}
