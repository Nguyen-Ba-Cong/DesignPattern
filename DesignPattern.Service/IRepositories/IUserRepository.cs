
using DesignPattern.Database.Entity;
using DesignPattern.Service.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IApiService
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User AddUser(User user);
        public List<New> GetNewByUserId(int id);
    }
}
