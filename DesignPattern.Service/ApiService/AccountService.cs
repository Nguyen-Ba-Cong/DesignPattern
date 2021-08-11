using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.Bcrypt;
using DesignPattern.Service.Common;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.IRepositories;
using DesignPattern.Service.JWT;
using DesignPattern.Service.Models;
using DesignPattern.Service.Models.JWT;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.ApiService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtService _jwtService;
        private readonly IBcrypt _bcrypt;
        private readonly IMapper _mapper;
        public AccountService(IAccountRepository accountRepository, IJwtService jwtService, IBcrypt bcrypt, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _jwtService = jwtService;
            _bcrypt = bcrypt;
            _mapper = mapper;
        }
        public ResponeTokenModel AdminLogin(string email, string password)
        {
            User user = _accountRepository.AuthenticateAdmin(email, _bcrypt.HashCode(password));
            ResponeTokenModel respone = new ResponeTokenModel();
            if (user != null)
            {
                respone.Token = _jwtService.GenerateJWT(email, user.Role);
                respone.Id = user.Id;
                respone.Scope = user.Role;
                return respone;
            }
            return null;
        }

        public string ChangePassword(string email, ChangePasswordModel model)
        {
            var user = _accountRepository.FindByEmail(email);
            if (user.Password != _bcrypt.HashCode(model.OldPassword))
            {
                return Constants.OldPasswordIncorrect;
            }
            else
            {
                user.Password = _bcrypt.HashCode(model.NewPassword);
                _accountRepository.UpdateUser(user);
                return Constants.Success;
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            var user = _accountRepository.FindByEmail(email);
            var userResult = _mapper.Map<UserModel>(user);
            return userResult;
        }

        public ResponeTokenModel GuestLogin(string email, string password)
        {
            User user = _accountRepository.AuthenticateGuest(email, _bcrypt.HashCode(password));
            ResponeTokenModel respone = new ResponeTokenModel();
            if (user != null)
            {
                respone.Token = _jwtService.GenerateJWT(email, user.Role);
                respone.Id = user.Id;
                respone.Scope = user.Role;
                return respone;
            }
            return null;
        }

        public void ResetPassword(string email, string newPassword, string code)
        {
            var user = _accountRepository.FindByEmail(email);
            if (user.ResetPasswordToken == code)
            {
                user.Password = _bcrypt.HashCode(newPassword);
                user.ResetPasswordToken = null;
                _accountRepository.UpdateUser(user);
            }
        }

        public void SaveResetpasswordToken(int userId, string token)
        {
            _accountRepository.SaveResetPasswordToken(userId, token);
        }
    }
}
