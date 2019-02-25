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
using Serilog;
using System.Text;
using System.Security.Cryptography;
using dTax.ViewModels;
using dTax.Auth;
using dTax.Common;
using dTax.Services;


namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseUtilsController
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

                string PasswordHash = GetHash(loginModel.Password);

                User user = await db.Users.Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == PasswordHash);


                if (user != null)
                {
                    ClaimsIdentity identity = GetIdentity(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    //if (user.FullReg != false)
                    //{
                    //    Driver driver = await db.Drivers.FirstOrDefaultAsync(d => d.UserId == user.Id);

                    //    //Cab cab = await db.Cabs.FirstOrDefaultAsync(c =>c.DriverId == driver.Id);

                    //    Shift shift = await db.Shifts.FirstOrDefaultAsync(s => s.DriverId == driver.Id);

                    //    shift.LoginTime = DateTime.Now;


                    //    db.Shifts.Update(shift);
                    //    await db.SaveChangesAsync();

                    //}

                }
                else
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                var response = new LoginViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

               // EmailService emailService = new EmailService();
               // await emailService.SendEmailAsync(loginModel.Email, "Авторизация в системе", String.Format("{0} {1} {2} {3}", "Проверка входа!", "Привет:", user.FirstName, user.LastName ));
                return Json(response);
            }
            catch (Exception)
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
                   .FirstOrDefaultAsync(u => u.Email == registerModel.Login || u.Email == registerModel.Email);

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
                        Email = registerModel.Email,
                        Password = PasswordHash,
                        BirthDate = registerModel.BirthDate,
                        RoleId = 1,//User
                        
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

                    var response = new LoginViewModel
                    {
                        Id = NewUser.Id,
                        Email = NewUser.Email,
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName
                    };

                    return Json(response);
                }
            }
            catch (Exception)
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






        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Json("");
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
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
        private ClaimsIdentity GetIdentity(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        new Claim(CustomClaimType.RoleName, user.Role.Name),
                        new Claim(CustomClaimType.UserName, user.FirstName),
                        new Claim(CustomClaimType.UserId, user.Id.ToString())
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

      

        #endregion
    }
}
