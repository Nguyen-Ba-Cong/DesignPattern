using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.Models;
using Microsoft.AspNetCore.Authorization;

namespace DesignPattern.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetCategories(int limit = 10, int offset = 0)
        {
            var response = _categoryService.GetCategories(offset, limit);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var response = _categoryService.GetCategory(id);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }
        [HttpGet("{id}/News")]
        public IActionResult GetNewByCategoryId(int id)
        {
            var response = _categoryService.GetNewByCategoryId(id);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, CategoryModel category)
        {
            var response = _categoryService.UpdateCategory(id, category);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.Error);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult PostCategory(CategoryModel category)
        {
            var response = _categoryService.AddCategory(category);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.Error);
        }
        [Authorize(Roles = "Admin")]

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id, CategoryModel categoryModel)
        {
            var response = _categoryService.DeleteCategory(id, categoryModel);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.Error);
        }

    }
}
