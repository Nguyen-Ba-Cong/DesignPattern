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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users?limit=10&offset=1
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
