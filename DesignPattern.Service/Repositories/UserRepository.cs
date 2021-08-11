using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.Bcrypt;
using DesignPattern.Service.IApiService;
using DesignPattern.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DesignPatternDBContext _context;
        private readonly IBcrypt _bcrypt;
        public UserRepository(DesignPatternDBContext context, IBcrypt bcrypt) : base(context)
        {
            _context = context;
            _bcrypt = bcrypt;
        }

        public User AddUser(User user)
        {
            try
            {
                if (!_context.Users.Contains(user))
                {
                    var newUser = new User();
                    newUser.UserName = user.UserName;
                    newUser.Role = "Guest";
                    newUser.Email = user.Email;
                    newUser.Password = _bcrypt.HashCode(user.Password);
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    return newUser;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<New> GetNewByUserId(int id)
        {
            try
            {
                var user = _context.Users.Include(u => u.News).FirstOrDefault(u => u.Id == id);
                List<New> news = new List<New>();
                foreach (var n in user.News)
                {
                    news.Add(n);
                }
                return news;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
