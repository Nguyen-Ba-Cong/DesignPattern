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
    /// To do something with User.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor
        /// </summary>

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users?limit=10&offset=1
        /// <summary>
        /// Get 10 Users.
        /// </summary>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="201">Return the newly created item.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="limit">The number of item user want to take</param>s
        /// <param name="offset">The point*10 you want begin ten take next 10 item</param>s
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUsers(int limit = 10, int offset = 0)
        {
            var response = _userService.GetUsers(offset, limit);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }

        // GET: api/Users/5
        /// <summary>
        /// Get news by category's id.
        /// </summary>
        /// <response code="401">If user is not authenticated.</response>
        /// <response code="200">Return the newly created item.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of user you want to take</param>s
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var response = _userService.GetUser(id);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }
        // GET: api/Users/5/News
        /// <summary>
        /// Get News by user who write it.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of Author</param>s
        [HttpGet("{id}/News")]
        public IActionResult GetNewByUserId(int id)
        {
            var response = _userService.GetNewByUserId(id);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(404, Service.Common.Constants.NotFound);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Update some information of user
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="user">New Info of user you want to update</param>
        [HttpPut("{id}")]
        public IActionResult PutUser(UserModel user)
        {
            var response = _userService.UpdateUser(user);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.Error);
        }

        // POST: api/Users
        /// <summary>
        /// Create New users.
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="user">Info of user you want to create</param>s
        [HttpPost]
        public IActionResult PostUser(UserModel user)
        {
            var response = _userService.AddUser(user);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.Error);

        }

        // DELETE: api/Users/5
        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="201">Return the newly created item.</response>
        /// <response code="400">Invalid request .</response>
        /// <param name="id">Id of user you want to delete</param>
        /// <param name="user">Info of user you want to delete</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id, UserModel user)
        {
            var response = _userService.DeleteUser(id, user);
            if (response != null)
            {
                return StatusCode(200, response);
            }
            return StatusCode(402, Service.Common.Constants.Error);
        }

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
