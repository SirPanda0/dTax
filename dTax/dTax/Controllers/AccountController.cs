using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dTax.Models;
using Microsoft.AspNetCore.Authorization;
using dTax.ApiModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace dTax.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private DbPostrgreContext db;

        public AccountController(DbPostrgreContext context)
        {
            db = context;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                User user = await db.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == loginModel.Password);

                if (user != null)
                {
                    await Authenticate(user);
                }
                else
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                return Json(user);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return BadRequest();
            }
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные");
                }

                if (registerModel.RoleId != 1)
                {
                    return BadRequest("Проверьте данные");
                }

                User NewUser = registerModel;
                await db.Users.AddAsync(NewUser);
                await db.SaveChangesAsync();

                return Json(NewUser);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return BadRequest();
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        //[Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [Authorize]
        [Route("Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User UserEdit = userModel;

            db.Users.Update(UserEdit);
            await db.SaveChangesAsync();
            return Ok(UserEdit);


        }



        #region Приватный регион
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "dTaxCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
        #endregion
    }
}
