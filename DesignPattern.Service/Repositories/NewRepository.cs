using DesignPattern.Database.Entity;
using DesignPattern.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Repositories
{
    public class NewRepository : BaseRepository<New>, INewRepository
    {
        private readonly DesignPatternDBContext _context;
        public NewRepository(DesignPatternDBContext context) : base(context)
        {
            _context = context;
        }

        public New AddNew(string email, New neww)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email);
                var newToAdd = new New();
                newToAdd.Title = neww.Title;
                newToAdd.Content = neww.Content;
                newToAdd.Image = neww.Image;
                newToAdd.Description = neww.Description;
                newToAdd.Users = user;
                _context.News.Add(newToAdd);
                _context.SaveChanges();
                return newToAdd;

            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
                return null;
            }
        }

        public New DeleteNew(string email, New neww)
        {
            try
            {
                var newDel = _context.News.Include(n => n.Users).FirstOrDefault(n => n.Id == neww.Id);
                if (newDel.Users.Email == email)
                {
                    _context.News.Remove(newDel);
                    _context.SaveChanges();
                    return newDel;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
            return null;
        }

        public New UpdateNew(string email, New neww)
        {
            try
            {
                var newUpdate = _context.News.Include(n => n.Users).FirstOrDefault(n => n.Id == neww.Id);
                if (newUpdate.Users.Email == email)
                {
                    newUpdate.Title = neww.Title;
                    newUpdate.Content = neww.Content;
                    newUpdate.Image = neww.Image;
                    newUpdate.Description = neww.Description;
                    _context.SaveChanges();
                    return newUpdate;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
            return null;
        }
    }
}
