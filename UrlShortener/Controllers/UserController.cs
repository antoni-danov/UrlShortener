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
        //[ServiceFilter(typeof(ValidationFiltersAttribute))]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterUserDto data)
        {
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

                return BadRequest(new RegistrationResponseDto { Errors = errors });

            }

            return StatusCode(201);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginUserDto data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userManager.FindByEmailAsync(data.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, data.Password))
            {
                return StatusCode(401, new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            }

            var result = await signInManager.PasswordSignInAsync(user.Email, data.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var signingCredentials = jwtHandler.GetSigningCredentials();
                var claims = jwtHandler.GetClaims(user);
                var tokenOptions = jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            
                return StatusCode(200, new AuthResponseDto { IsAuthSuccessful = true, Token = token });
            }

            return Ok();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return StatusCode(205);
        }

    }
}
