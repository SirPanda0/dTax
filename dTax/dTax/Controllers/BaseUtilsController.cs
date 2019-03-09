using dTax.Common;
using dTax.Interfaces;
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

        protected decimal CalculateBookPrice(int dist)
        {
            if (dist != 0)
            { 
            if (DateTime.Now.Hour < 17 && DateTime.Now.Hour > 8)
                return (dist * 9) + 50;
            else
            if (DateTime.Now.Hour > 17)
                return (dist * 11) + 50;
            }
            return 50;
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
