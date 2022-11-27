using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using UrlShortener.Models.JwtFeatures;
using UrlShortener.Models.UserDtos.Login;
using UrlShortener.Models.UserDtos.Registration;

namespace UrlShortener.Services.UserService
{
    public class UserService : IUserService
    {
        private const string userExist = "User already exist";
        private const string authenticationFailed = "Wrong Email or Password";

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly JwtHandler jwtHandler;


        public UserService(UserManager<IdentityUser> usermanager,
                           SignInManager<IdentityUser> signInManager,
                           JwtHandler jwtHandler)
        {
            this.userManager = usermanager;
            this.signInManager = signInManager;
            this.jwtHandler = jwtHandler;
        }
        public async Task<AuthResponseDto> CreateUser(RegisterUserDto data)
        {
            IdentityUser currentUser = new IdentityUser
            {
                UserName = data.Email,
                Email = data.Email
            };

            IdentityUser existingUser = await userManager.FindByEmailAsync(data.Email);

            if (existingUser == null)
            {
                IdentityResult newUser = await userManager.CreateAsync(currentUser, data.Password);

                if (newUser.Succeeded)
                {
                    var result = JWTTokenFabric(currentUser);

                    return new AuthResponseDto { IsAuthSuccessful = true, Token = result };
                }

                List<string> errors = newUser.Errors.Select(d => d.Description).ToList();

                return new AuthResponseDto { RegistrationErrors = errors };
            }

            return new AuthResponseDto { RegistrationErrors = new List<string>(){userExist} };

        }
        public async Task<AuthResponseDto> LoginUser(LoginUserDto data)
        {

            IdentityUser user = await userManager.FindByEmailAsync(data.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, data.Password))
            {
                return new AuthResponseDto { RegistrationErrors = new List<string>(){authenticationFailed} };
            }

            SignInResult result = await signInManager.PasswordSignInAsync(user.Email, data.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var token = JWTTokenFabric(user);

                AuthResponseDto finalResult = new AuthResponseDto { IsAuthSuccessful = true, Token = token };

                return finalResult;
            }

            return new AuthResponseDto { RegistrationErrors = new List<string>() { authenticationFailed } };
        }
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
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
