using dTax.Common;
using dTax.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    public class BaseUtilsController : Controller
    {
        protected string GetHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        protected PriceView CalculateBookPrice(int dist)
        {
            if (dist != 0)
            {
                if (DateTime.Now.Hour <= 17 && DateTime.Now.Hour >= 8)
                    return new PriceView
                    {
                        Standart = (dist * 12) + 60,
                        Comfort = (dist * 12) + 90,
                        Emergency = (dist * 15) + 90,
                        Minivan = (dist * 18) + 60
                    };
                else
                if (DateTime.Now.Hour > 17)
                    return new PriceView
                    {
                        Standart = (dist * 13) + 70,
                        Comfort = (dist * 13) + 100,
                        Emergency = (dist * 16) + 100,
                        Minivan = (dist * 19) + 70
                    };
            }
                return new PriceView
                {
                    Standart = 60,
                    Comfort = 90,
                    Emergency = 90,
                    Minivan = 60
                };
           
        }


        protected Guid GetUserIdByContext()
        {
            return Guid.Parse(HttpContext.User.FindFirst(c => c.Type == CustomClaimType.UserId).Value);
        }


        protected object GetPagingCollections<T>(IEnumerable<T> collection, int page = 1, int size = 10) where T : class
        {
            var count = collection.Count();

            var pager = new Pager(count, page, size);
            var skip = (pager.CurrentPage - 1) * pager.PageSize;
            var maxPage = count / size;

            return new
            {
                Collection = collection.Skip(skip).Take(pager.PageSize),
                Pager = pager
            };

        }
    }
}
