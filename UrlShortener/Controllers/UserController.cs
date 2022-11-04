using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public UserController(IUserService userService,
            UserManager<IdentityUser> usermanager,
                           SignInManager<IdentityUser> signInManager)
        {
            this.userService = userService;
            this.userManager = usermanager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        //[ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto data)
        {
            //await userService.CreateUser(data);

            //return StatusCode(201);

            if (data == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUser = new IdentityUser
            {
                UserName = data.Email,
                Email = data.Email
            };

            var newUser = await userManager.CreateAsync(currentUser, data.Password);

            if (!newUser.Succeeded)
            {
                var errors = newUser.Errors.Select(d => d.Description);

                return BadRequest(new RegistrationResponseDto {Errors = errors});

            }

            return StatusCode(201);
        }

        [HttpPost("test")]

        public async Task<IActionResult> Test([FromBody] RegisterUserDto data)
        {
            //var user = await userManager.FindByEmailAsync(data.Email);

            //if (user != null && await userManager.CheckPasswordAsync(user, data.Password))
            //{
            //    var result = await signInManager.PasswordSignInAsync(user.UserName, data.Password, isPersistent: false, lockoutOnFailure: false);

            //    if (result.Succeeded)
            //    {
            //        return Redirect("https://www.google.com");

            //    }

            //}

            return Redirect("https://www.abv.bg");
        }

    }
}
