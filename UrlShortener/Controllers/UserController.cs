using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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
                List<string> errorsMessage = ModelState.Values
                    .Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errorsMessage);
            }

            AuthResponseDto result = await userService.CreateUser(data);

            if (result.IsAuthSuccessful == true)
            {
                return StatusCode(201, new AuthResponseDto { IsAuthSuccessful = true, Token = result.Token });
            }

            //List<string> errors = result.RegistrationErrors!.Select(d => d).ToList();

            return BadRequest(result.RegistrationErrors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .Where(e => e.Errors.Count > 0)
                    .SelectMany(e => e.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }

            AuthResponseDto result = await userService.LoginUser(data);

            if (result.IsAuthSuccessful)
            {
                return StatusCode(200, new AuthResponseDto { IsAuthSuccessful = true, Token = result.Token });
            }

            return StatusCode(401, result.RegistrationErrors);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult GoogleLogin(string provider, string returnUrl = null)
        //{
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        //    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return Challenge(properties, provider);
        //}
           var user = new IdentityUser { Email = "", UserName = "" }; var token = JWTTokenFabric(user);
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token}); ;
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await this.userService.Logout();
            return StatusCode(205);
        }

        private string JWTTokenFabric(IdentityUser currentUser)
        {
            var signingCredentials = jwtHandler.GetSigningCredentials();
            var claims = jwtHandler.GetClaims(currentUser);
            var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

    }
}
