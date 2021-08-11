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
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewService _newSerVice;

        public NewsController(INewService newSerVice)
        {
            _newSerVice = newSerVice;
        }

        // GET: api/News
        [HttpGet]
        public List<NewModel> GetNews(int offset = 0, int limit = 10)
        {
            return _newSerVice.GetNews(offset, limit);
        }

        // get: api/news/5
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
