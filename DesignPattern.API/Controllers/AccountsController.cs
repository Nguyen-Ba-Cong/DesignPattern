using DesignPattern.Service.IApiServices;
using DesignPattern.Service.Models.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using DesignPattern.Service.Email;
using DesignPattern.Service.Models;
using DesignPattern.Service.Models.ResetPassword;

namespace DesignPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISendMail _sendmail;
        public AccountsController(IAccountService accountService, ISendMail sendmail)
        {
            _accountService = accountService;
            _sendmail = sendmail;
        }
        [HttpPost("Admin/Login")]
        public IActionResult AdminLogin(AccountModel accountModel)
        {
            var response = _accountService.AdminLogin(accountModel.Email, accountModel.Password);
            if (response == null)
            {
                return StatusCode(404, Service.Common.Constants.NotFound);
            }
            else
            {
                return StatusCode(200, response);
            }
        }
        [HttpPost("Guest/Login")]
        public IActionResult GuestLogin(AccountModel accountModel)
        {
            var response = _accountService.GuestLogin(accountModel.Email, accountModel.Password);
            if (response == null)
            {
                return StatusCode(404, Service.Common.Constants.NotFound);
            }
            else
            {
                return StatusCode(200, response);
            }
        }
        [HttpPut("Change-Password")]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var email = currentEmail();
            var response = _accountService.ChangePassword(email, changePasswordModel);
            return StatusCode(200, response);
        }
        [HttpPost("Forgot-Password")]
        public IActionResult ForgotPassword(SendMailModel email)
        {
            var user = _accountService.GetUserByEmail(email.Email);
            if (user != null)
            {
                Random rand = new Random();
                var token = rand.Next(1000, 9999);
                try
                {
                    _accountService.SaveResetpasswordToken(user.Id, token.ToString());
                    string content = "This is your reset Password Code: " + token.ToString();
                    string subject = "Reset Password";
                    _sendmail.SendMailMethod(email.Email, content, subject);
                    return StatusCode(200, Service.Common.Constants.Success);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                    throw;
                }
            }
            return StatusCode(500, Service.Common.Constants.Error);
        }
        [HttpPut("Reset-Password")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                _accountService.ResetPassword(resetPasswordModel.Email, resetPasswordModel.Password, resetPasswordModel.Code);
                return StatusCode(500, Service.Common.Constants.Success);
            }
            catch (Exception)
            {
                return StatusCode(500, Service.Common.Constants.Error);
            }
        }
        private string currentEmail()
        {
            return User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
        }
    }
}
