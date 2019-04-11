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
    public class CarBrandController : BaseUtilsController
    {
        private ICarBrandRepository carBrandRepository;
        public CarBrandController(ICarBrandRepository injectedcarBrandRepository)
        {
            carBrandRepository = injectedcarBrandRepository;
        }

        //[PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var list = carBrandRepository.GetListBrands();
          return Json(list);
        }

    }
}
