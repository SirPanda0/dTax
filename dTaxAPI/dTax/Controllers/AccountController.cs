using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dTax.Entity;
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
using dTax.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace dTax.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseUtilsController
    {
        private IDBWorkFlow DBWorkflow;
        public AccountController(IDBWorkFlow dBWorkFlow)
        {
            DBWorkflow = dBWorkFlow;
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
                User user = DBWorkflow.UserRepository.FindUserLogin(loginModel.Email, PasswordHash);


                if (user != null)
                {
                    ClaimsIdentity identity = GetIdentity(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    //TODO Обновление времени логина в смене для водителя
                    //if (user.FullReg != false)
                    //{
                    //    Driver driver = await db.Drivers.FirstOrDefaultAsync(d => d.UserId == user.Id);
                    //    //Cab cab = await db.Cabs.FirstOrDefaultAsync(c =>c.DriverId == driver.Id);
                    //    Shift shift = await db.Shifts.FirstOrDefaultAsync(s => s.DriverId == driver.Id);
                    //    shift.LoginTime = DateTime.Now;
                    //    db.Shifts.Update(shift);
                    //    await db.SaveChangesAsync();
                    //}
                    if (user.FullReg != false && user.RoleId == 3)
                    {
                        var driver = DBWorkflow.DriverRepository.GetDriverByUserId(user.Id);
                        var cab = DBWorkflow.CabRepository.GetCabByDriverId(driver.Id);
                        var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                        if (shift == null)
                        {
                            DBWorkflow.ShiftRepository.Insert(new Shift
                            {
                                DriverId = driver.Id,
                                CabId = cab.Id,
                                LoginTime = DateTime.Now
                            });
                        }
                        else
                        {
                            shift.LoginTime = DateTime.Now;
                            DBWorkflow.ShiftRepository.Update(shift);
                        }
                        DBWorkflow.ShiftRepository.Commit();
                    }


                    var response = new LoginResponseModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        RoleId = user.Role.Id
                    };

                    //TODO определение IP пользователя для рассылки на Email при входе
                    //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    //if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    //    remoteIpAddress = Request.Headers["X-Forwarded-For"];

                    //string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    //При тестировании комментировать
                    //var emailService = new EmailService(DBWorkflow);
                    //await emailService.AuthEmailAsync(user.Email);

                    return Json(response);
                }
                else
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
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

                bool exist = DBWorkflow.UserRepository.IsUserExists(registerModel.Email);

                if (exist)
                {
                    return BadRequest("Пользователь с таким Email уже существует");
                }

                if (registerModel.RoleId == (int)AuthenticationRole.Operator)
                {
                    return BadRequest("Проверьте данные");
                }

                bool IsFull = false;

                switch (registerModel.RoleId)
                {
                    case (int)AuthenticationRole.Driver:
                        IsFull = false;
                        break;
                    case (int)AuthenticationRole.User:
                        IsFull = true;
                        break;
                    default:
                        break;
                }

                string PasswordHash = GetHash(registerModel.Password);

                User user = new User()
                {
                    Email = registerModel.Email,
                    Password = PasswordHash,
                    FirstName = registerModel.FirstName,
                    MiddleName = registerModel.MiddleName,
                    LastName = registerModel.LastName,
                    PhoneNumber = registerModel.PhoneNumber,
                    RoleId = registerModel.RoleId,
                    FullReg = IsFull,
                    BirthDate = registerModel.BirthDate
                };

                DBWorkflow.UserRepository.Insert(user);
                DBWorkflow.UserRepository.Commit();



                LoginResponseModel responseModel = new LoginResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId
                };

                if (IsFull)
                {
                    Customer customer = new Customer
                    {
                        UserId = user.Id
                    };

                    DBWorkflow.CustomerRepository.Insert(customer);
                    DBWorkflow.CustomerRepository.Commit();
                }

                ClaimsIdentity identity = GetIdentity(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                //TODO Сервис отправки сообщения при регистрации
                //var emailService = new EmailService(DBWorkflow);
                //await emailService.AuthEmailAsync(user.Email);



                return Json(responseModel);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }

        }


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
                        //new Claim(CustomClaimType.RoleName, user.Role.Name),
                        new Claim(CustomClaimType.UserName, user.FirstName),
                        new Claim(CustomClaimType.UserId, user.Id.ToString()),
                        new Claim(CustomClaimType.FullAccess , user.FullReg.ToString())
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