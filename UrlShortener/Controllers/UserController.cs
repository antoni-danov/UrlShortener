using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.JwtFeatures;
using UrlShortener.Models.UserDtos.Login;
using UrlShortener.Models.UserDtos.Registration;
using UrlShortener.Services.UserService;

namespace UrlShortener.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly JwtHandler jwtHandler;

        public UserController(IUserService userService,
            UserManager<IdentityUser> usermanager,
                           SignInManager<IdentityUser> signInManager,
                           JwtHandler jwtHandler)
        {
            this.userService = userService;
            this.userManager = usermanager;
            this.signInManager = signInManager;
            this.jwtHandler = jwtHandler;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            AuthResponseDto result = await userService.CreateUser(data);

            if (result.IsAuthSuccessful == true)
            {
                return StatusCode(201, new AuthResponseDto { IsAuthSuccessful = true, Token = result.Token });
            }
            if (result.RegistrationErrors != null)
            {
                var errors = result.RegistrationErrors.Select(d => d);

                return BadRequest(new AuthResponseDto { RegistrationErrors = errors });
            }

            return StatusCode(200, result);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            AuthResponseDto result = await userService.LoginUser(data);

            if (result.IsAuthSuccessful)
            {
                return StatusCode(200, new AuthResponseDto { IsAuthSuccessful = true, Token = result.Token });
            }

            return StatusCode(401, new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return StatusCode(205);
        }

    }
}
