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
    /// <summary>
    /// To do Add, Update , Get, Delete Category and Get News By Category Id.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        /// <summary>
        /// Constructor
        /// </summary>

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <response code="404">Result is not found.</response>
        /// <response code="400">Invalid request .</response>
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
        /// <summary>
        /// Get Category by Id.
        /// </summary>
        /// <response code="404">Result is not found.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of cateogy to get</param>
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
        /// <summary>
        /// Get news by category's id.
        /// </summary>
        /// <response code="404">Result is not found.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of category to get New</param>
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
        /// <summary>
        /// Update a specific CategoryItem.
        /// </summary>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of category to update</param>
        /// <param name="category">Category object with information need to be updated</param>
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
        /// <summary>
        /// Add new CategoryItem.
        /// </summary>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="200">Return the newly created item.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="category">Category object that need to be added.</param>

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
        /// <summary>
        /// Add new CategoryItem.
        /// </summary>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="200">Return the delete item.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="categoryModel">Category object that need to be added.</param>s
        /// <param name="id">Id of category select to make sure delete correct item</param>s
        [Authorize(Roles = "Admin")]
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
