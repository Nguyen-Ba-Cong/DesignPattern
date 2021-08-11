using AutoMapper;
using DesignPattern.Database.Entity;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.IRepositories;
using DesignPattern.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.ApiService
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public CategoryModel AddCategory(CategoryModel category)
        {
            var categoryAdd = _mapper.Map<Category>(category);
            var categoryResult = _categoryRepository.Create(categoryAdd);
            return category;
        }

        public CategoryModel DeleteCategory(int id, CategoryModel categoryModel)
        {
            if (categoryModel.Id == id)
            {
                var categoryDel = _mapper.Map<Category>(categoryModel);
                _categoryRepository.Delete(categoryDel);
                return categoryModel;
            }
            return null;

        }

        public List<CategoryModel> GetCategories(int offset, int limit)
        {
            var categories = _categoryRepository.All(offset, limit);
            var categoriesResult = _mapper.Map<List<CategoryModel>>(categories);
            return categoriesResult;
        }

        public CategoryModel GetCategory(int id)
        {
            var category = _categoryRepository.FindByCondition(x => x.Id == id).FirstOrDefault();
            var categoryResult = _mapper.Map<CategoryModel>(category);
            return categoryResult;
        }

        public List<NewModel> GetNewByCategoryId(int id)
        {
            var list = _categoryRepository.GetNewByCategoryId(id);
            var listResult = _mapper.Map<List<NewModel>>(list);
            return listResult;
        }

        public CategoryModel UpdateCategory(int id, CategoryModel category)
        {
            if (category.Id == id)
            {
                var categoryToUpdate = _mapper.Map<Category>(category);
                _categoryRepository.Update(categoryToUpdate);
                return category;
            }
            else
            {
                return null;
            }

        }
    }
}
