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
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System.Text;
using System.Security.Cryptography;
using dTax.ViewModels;

namespace dTax.Controllers
{
    [Route("[controller]")]
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

                var PasswordHash = GetHash(loginModel.Password);

                User user = await db.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Login == loginModel.Login && u.Password == PasswordHash);


                if (user != null)
                {
                    await Authenticate(user);

                    if (user.IsDriver != false && user.FullReg != false)
                    {
                        Driver driver = await db.Drivers.FirstOrDefaultAsync(d => d.UserId == user.Id);

                        //Cab cab = await db.Cabs.FirstOrDefaultAsync(c =>c.DriverId == driver.Id);

                        Shift shift = await db.Shifts.FirstOrDefaultAsync(s => s.DriverId == driver.Id);

                        shift.LoginTime = DateTime.Now;

                        db.Shifts.Update(shift);
                        await db.SaveChangesAsync();

                    }

                }
                else
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                return Json(user);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }




        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Проверьте данные");
                }

                User user = await db.Users.Include(u => u.Role)
                   .FirstOrDefaultAsync(u => u.Login == registerModel.Login || u.Email == registerModel.Email);

                if (user != null)
                {
                    return BadRequest("Такой пользователь уже существует!");
                }
                else
                {

                    var PasswordHash = GetHash(registerModel.Password);
                    bool full = false;

                    if (registerModel.IsDriver != true)
                    {
                        full = true;
                    }

                    User NewUser = new User
                    {
                        FirstName = registerModel.FirstName,
                        LastName = registerModel.LastName,
                        Login = registerModel.Login,
                        Password = PasswordHash,
                        BirthDate = registerModel.BirthDate,
                        RoleId = 1,//User
                        Email = registerModel.Email,
                        IsDriver = registerModel.IsDriver,
                        FullReg = full
                    };

                    await db.Users.AddAsync(NewUser);
                    await db.SaveChangesAsync();

                    if (full == true)
                    {
                        Customer rCustomer = new Customer { UserId = NewUser.Id };

                        await db.Customers.AddAsync(rCustomer);
                        await db.SaveChangesAsync();
                    }

                    return Json(NewUser);
                }
            }
            catch (Exception e)
            {

                return BadRequest();
            }
        }

        //[Authorize]
        //[Route("CustomeReg")]
        //[HttpPost]
        //public async Task<IActionResult> CustomeReg([FromQuery] int Id)
        //{

        //    User user = await db.Users
        //                .FirstOrDefaultAsync(u => u.Id == Id && u.IsDriver == false);

        //    Customer customer = await db.Customers.FirstOrDefaultAsync(u => u.UserId == Id);

        //    if (user != null && customer == null)
        //    {
        //        Customer rCustomer = new Customer { UserId = Id };

        //        await db.Customers.AddAsync(rCustomer);
        //        await db.SaveChangesAsync();
        //        return Ok("Успешно!");
        //    }
        //    else
        //        return BadRequest("Проверьте данные!");
        //}


        



        [ApiExplorerSettings(IgnoreApi = true)]
        //[Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        //TODO
        //[Authorize]
        //[Route("Edit")]
        //[HttpPut]
        //public async Task<IActionResult> Edit([FromBody] UserModel userModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    User UserEdit = new User
        //    {
        //        Email = userModel.Email,
        //        BirthDate = userModel.BirthDate,
        //        FirstName = userModel.FirstName,
        //        LastName = userModel.LastName,
        //        Password = userModel.Password
        //    };

        //    db.Users.Update(UserEdit);
        //    await db.SaveChangesAsync();
        //    return Ok(UserEdit);

        //}

        ///TODO
        //[Authorize]
        //[Route("GetUserInfo")]
        //[HttpGet]
        //public async Task<IActionResult> GetUser([FromQuery] int Id)
        //{
        //    User user = await db.Users.FirstOrDefaultAsync(i => i.Id == Id);

        //    UserViewModel userViewModel = new UserViewModel
        //    {
        //        Email = user.Email,
        //        BirthDate = user.BirthDate,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName
        //    };

        //    return Json(userViewModel);


        //}



        #region Приватный регион
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "dTaxCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        protected string GetHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        #endregion
    }
}
