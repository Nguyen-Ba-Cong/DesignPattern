using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesignPattern.Service.IApiServices;
using DesignPattern.Service.Models;
using System.Security.Claims;

namespace DesignPattern.API.Controllers
{
    /// <summary>
    /// Do CRUD with News.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewService _newSerVice;
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsController(INewService newSerVice)
        {
            _newSerVice = newSerVice;
        }

        // GET: api/News
        /// <summary>
        /// Get All News
        /// </summary>
        /// <response code="404">Not Found News</response>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="offset">The point begin to take next limit item</param>
        /// <param name="limit">The numbers of item to get</param>
        [HttpGet]
        public List<NewModel> GetNews(int offset = 0, int limit = 10)
        {
            return _newSerVice.GetNews(offset, limit);
        }

        // get: api/news/5
        /// <summary>
        /// Get All News
        /// </summary>
        /// <response code="404">Not Found New</response>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Get New By Id</param>
        [HttpGet("{id}")]
        public IActionResult GetNew(int id)
        {
            var response = _newSerVice.GetNew(id);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }

        // PUT: api/News/5
        /// <summary>
        /// Update New
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="newModel">New you want update</param>
        [HttpPut("{id}")]
        public IActionResult PutNew(NewModel newModel)
        {
            var email = currentEmail();
            var response = _newSerVice.UpdateNew(email, newModel);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.NotHavePermission);
        }

        // POST: api/News
        /// <summary>
        /// Create New
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="newModel">Info of new</param>
        [HttpPost]
        public IActionResult PostNew(NewModel newModel)
        {
            var email = currentEmail();
            var response = _newSerVice.AddNew(email, newModel);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.NotHavePermission);
        }

        // DELETE: api/News/5
        /// <summary>
        /// Delete New By Id
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <response code="401">User is not authenticated.</response>
        /// <param name="newModel">Info of new</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteNew(NewModel newModel)
        {
            var email = currentEmail();
            var response = _newSerVice.DeleteNew(email, newModel);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.NotHavePermission);
        }

        private string currentEmail()
        {
            return User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
        }
    }
}
