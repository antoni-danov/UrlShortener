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
        private IUserService userService;

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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await userService.DeleteUrl(id);

            return StatusCode(200);
        }

        
    }
}
