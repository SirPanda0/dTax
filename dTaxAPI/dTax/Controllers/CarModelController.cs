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
    public class CarModelController : BaseUtilsController
    {
        private ICarModelRepository carModelRepository;
        public CarModelController(ICarModelRepository injectedcarModelRepository)
        {
            carModelRepository = injectedcarModelRepository;
        }

        [PolicyAuthorize(AuthorizePolicy.Driver)]
        [HttpGet]
        [Route("GetModelsByBrand")]
        public IActionResult Get(Guid brandId)
        {
            var list = carModelRepository.GetListModelsByBrandId(brandId);
            return Json(list);
        }
    }
}
