using DesignPattern.Database.Entity;
using DesignPattern.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IApiServices
{
    public interface IUserService
    {
        public List<UserModel> GetUsers(int offset, int limit);
        public UserModel GetUser(int id);
        public UserModel AddUser(UserModel user);
        public UserModel DeleteUser(int id, UserModel user);
        public UserModel UpdateUser(UserModel user);
        public List<NewModel> GetNewByUserId(int id);
    }
}
