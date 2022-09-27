using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Services.UserService;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public List<UrlData> GetAll()
        {
            var result = userService.GetAll();
            return result;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUrlById([FromRoute] int id)
        {
            var url = userService.GetUrlById(id);

            return StatusCode(200, url);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public IActionResult CreateUser(User data)
        {
           var result =  this.userService.CreateUser(data);

            return StatusCode(201, result);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            userService.DeleteUrl(id);

            return StatusCode(200);
        }

        
    }
}
