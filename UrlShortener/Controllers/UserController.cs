using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Services.UserService;

namespace UrlShortener.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        public IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("search/all/{uid}")]
        public async Task<ActionResult<List<UrlData>>> GetAll([FromRoute] string uid)
        {
            var result = await userService.GetAll(uid);
            return result.ToList();

        }
        
        [HttpGet("{id}")]
        public IActionResult GetUrlById([FromRoute] int id)
        {
            var url = userService.GetUrlById(id);

            return StatusCode(200, url);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> CreateUserAsync(User data)
        {
            this.userService.CreateUser(data);

            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            userService.DeleteUrl(id);

            return StatusCode(200);
        }

        
    }
}
