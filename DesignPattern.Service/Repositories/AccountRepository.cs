using DesignPattern.Database.Entity;
using DesignPattern.Service.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DesignPatternDBContext _context;
        public AccountRepository(DesignPatternDBContext context)
        {
            _context = context;
        }
        public User AuthenticateAdmin(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == "Admin");
            return user;
        }

        public User AuthenticateGuest(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == "Guest");
            return user;
        }
        public User FindByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public async Task SaveResetPasswordToken(int id, string token)
        {
            var user = await _context.Users.FindAsync(id);
            user.ResetPasswordToken = token;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            var userUpdate = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            userUpdate.Password = user.Password;
            _context.Users.Update(userUpdate);
            _context.SaveChanges();
        }
    }
}
