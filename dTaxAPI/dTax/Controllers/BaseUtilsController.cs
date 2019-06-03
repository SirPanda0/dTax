using dTax.Common;
using dTax.Entity.Models.Users;
using dTax.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace dTax.Controllers
{
    public class BaseUtilsController : Controller
    {
        protected string GetHash(string password)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return null;
            }
            
        }

        protected PriceView CalculateBookPrice(int dist)
        {
            try
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
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return null;
            }

        }


        protected Guid GetUserIdByContext()
        {
            try
            {
                return Guid.Parse(HttpContext.User.FindFirst(c => c.Type == CustomClaimType.UserId).Value);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return Guid.Empty;
            }
        }


        protected object GetPagingCollections<T>(IEnumerable<T> collection, int page = 1, int size = 10) where T : class
        {
            try
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
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return null;
            }
        }

        

        protected ClaimsIdentity GetIdentity(UserEntity user)
        {
            try
            {
                var claims = new List<Claim>
                {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        //new Claim(CustomClaimType.RoleName, user.Role.Name),
                        new Claim(CustomClaimType.UserName, user.FirstName),
                        new Claim(CustomClaimType.UserId, user.Id.ToString()),
                        new Claim(CustomClaimType.FullAccess , user.IsFullReg.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "dTaxCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                return claimsIdentity;
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return null;
            }

        }

    }
}
