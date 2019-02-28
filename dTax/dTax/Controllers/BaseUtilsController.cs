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

        protected Guid GetUserIdByContext()
        {
            return Guid.Parse(HttpContext.User.FindFirst(c => c.Type == CustomClaimType.UserId).Value);
        }
    }
}
