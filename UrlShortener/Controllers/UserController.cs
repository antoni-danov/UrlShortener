using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<ActionResult<User>> CreateUserAsync(User data)
        {
            var createdUser = await userService.CreateUser(data);

            return StatusCode(201, createdUser);
        }
        
    }
}
