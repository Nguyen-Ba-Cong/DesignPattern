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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly DesignPatternDBContext _context;
        public CategoryRepository(DesignPatternDBContext context) : base(context)
        {
            _context = context;
        }

        public List<New> GetNewByCategoryId(int id)
        {
            List<New> news = new List<New>();
            var category = _context.Categories.Include(c => c.News).Where(c => c.Id == id).FirstOrDefault();
            foreach (var n in category.News)
            {
                news.Add(n);
            }
            return news;
        }
        //public Category AddCategory(Category category)
        //{
        //    try
        //    {
        //        _context.Categories.Add(category);
        //        _context.SaveChanges();
        //        return category;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(JsonConvert.SerializeObject(e));
        //    }
        //    return null;
        //}

        //public Category DeleteCategory(int id)
        //{
        //    try
        //    {
        //        var categoryDel = GetCategory(id);
        //        if (categoryDel == null)
        //        {
        //            Console.WriteLine("Category Not Found");
        //            return null;
        //        }
        //        else
        //        {
        //            _context.Categories.Remove(categoryDel);
        //            return categoryDel;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(JsonConvert.SerializeObject(e));
        //    }
        //    return null;
        //}

        //public List<Category> GetCategories()
        //{
        //    try
        //    {
        //        var categories = _context.Categories.ToList();
        //        return categories;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(JsonConvert.SerializeObject(e));
        //    }
        //    return null;
        //}

        //public Category GetCategory(int id)
        //{
        //    try
        //    {
        //        var category = _context.Categories.FirstOrDefault(c => c.Id == id);
        //        return category;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(JsonConvert.SerializeObject(e));
        //    }
        //    return null;
        //}

        //public Category UpdateCategory(int id, Category category)
        //{
        //    try
        //    {
        //        if (category.Id != id)
        //        {
        //            Console.WriteLine("Id not match");
        //            return null;
        //        }
        //        var categoryUpdate = GetCategory(id);
        //        categoryUpdate.Name = category.Name;
        //        _context.SaveChanges();
        //        return categoryUpdate;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(JsonConvert.SerializeObject(e));
        //    }
        //    return null;
        //}
    }
}
