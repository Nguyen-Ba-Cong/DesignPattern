using DesignPattern.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.IApiServices
{
    public interface ICategoryService
    {
        public List<CategoryModel> GetCategories(int offset, int limit);
        public CategoryModel GetCategory(int id);
        public CategoryModel AddCategory(CategoryModel category);
        public CategoryModel UpdateCategory(int id, CategoryModel category);
        public CategoryModel DeleteCategory(int id, CategoryModel categoryModel);
        public List<NewModel> GetNewByCategoryId(int id);
    }
}
