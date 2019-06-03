using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using Microsoft.AspNetCore.Http;
using System.Net;
using dTax.Data.Interfaces;
using dTax.Entity.Models.Users;
using dTax.Entity.Models.Shifts;
using dTax.Entity.Models.Customers;
using System.Net.Mail;
using dTax.Common.Enums;

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

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
               
                var user = DBWorkflow.UserRepository.GetUserById(GetUserIdByContext());

                var response = new UserView
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    PhoneNumber = user.PhoneNumber,
                    IsFullReg = user.IsFullReg
                };

                return Json(response);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("Update")]
        [HttpPut]
        public ActionResult Update([FromBody] UserModel userModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                var user = DBWorkflow.UserRepository.GetUserById(GetUserIdByContext());
                
                if (userModel.NewPassword != null && userModel.OldPassword != null)
                {
                    string OldPasswordHash = GetHash(userModel.OldPassword);
                    string NewPasswordHash = GetHash(userModel.NewPassword);

                    if (OldPasswordHash == user.Password)
                    {
                        user.Password = NewPasswordHash;
                    }
                    else
                    {
                        return BadRequest("Проверьте пароль!");
                    }

                }

                user.Email = userModel.Email;
                user.PhoneNumber = userModel.PhoneNumber;

                DBWorkflow.UserRepository.Update(user);
                DBWorkflow.UserRepository.Commit();

                return StatusCode(200);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        [Authorize]
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var user = DBWorkflow.UserRepository.GetUserById(GetUserIdByContext());

                user.IsDeleted = true;
                DBWorkflow.UserRepository.Update(user);
                DBWorkflow.UserRepository.Commit();

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return StatusCode(200);
            }
            catch(Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Некорректные логин и(или) пароль");
                }

                string PasswordHash = GetHash(loginModel.Password);
                UserEntity user = DBWorkflow.UserRepository.FindUserLogin(loginModel.Email, PasswordHash);

                if (user != null)
                {
                    ClaimsIdentity identity = GetIdentity(user);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    if (user.IsFullReg != false && user.RoleId == (int)AuthenticationRole.Driver)
                    {
                        var driver = DBWorkflow.DriverRepository.GetDriverByUserId(user.Id);
                        var cab = DBWorkflow.CabRepository.GetCabByDriverId(driver.Id);
                        var shift = DBWorkflow.ShiftRepository.GetShiftByDriverId(driver.Id);

                        if (shift == null)
                        {
                            DBWorkflow.ShiftRepository.Insert(new ShiftEntity
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

                    var response = new UserView
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        MiddleName = user.MiddleName,
                        LastName = user.LastName,
                        RoleId = user.RoleId,
                        PhoneNumber = user.PhoneNumber,
                        IsFullReg = user.IsFullReg
                    };

                    //TODO определение IP пользователя для рассылки на Email при входе
                    //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                    //if (Request.Headers.ContainsKey("X-Forwarded-For"))
                    //    remoteIpAddress = Request.Headers["X-Forwarded-For"];
                    //string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                    //При тестировании комментировать
                    // var emailService = new EmailService(DBWorkflow);
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

                UserEntity user = new UserEntity()
                {
                    Email = registerModel.Email,
                    Password = PasswordHash,
                    FirstName = registerModel.FirstName,
                    MiddleName = registerModel.MiddleName,
                    LastName = registerModel.LastName,
                    PhoneNumber = registerModel.PhoneNumber,
                    RoleId = registerModel.RoleId,
                    IsFullReg = IsFull,
                    BirthDate = registerModel.BirthDate
                };

                DBWorkflow.UserRepository.Insert(user);
                DBWorkflow.UserRepository.Commit();



                var response = new UserView
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    PhoneNumber = user.PhoneNumber
                };

                if (IsFull)
                {
                    CustomerEntity customer = new CustomerEntity
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
                //await emailService.RegEmailAsync(user.Email);

                return Json(response);
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

                return StatusCode(200);
            }
            catch (Exception e)
            {
                Log.Error("\nMessageError: {0} \n StackTrace: {1}", e.Message, e.StackTrace);
                return StatusCode(500);
            }
        }

        #region Приватный регион
       



        #endregion
    }
}