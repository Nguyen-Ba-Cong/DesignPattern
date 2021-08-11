using DesignPattern.Service.Models;
using DesignPattern.Service.Models.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IApiServices
{
    public interface IAccountService
    {
        public ResponeTokenModel AdminLogin(string email, string password);
        public ResponeTokenModel GuestLogin(string email, string password);
        public void SaveResetpasswordToken(int userId, string token);
        public void ResetPassword(string email, string newPassword, string code);
        public string ChangePassword(string email, ChangePasswordModel model);

        public UserModel GetUserByEmail(string email);

    }
}
