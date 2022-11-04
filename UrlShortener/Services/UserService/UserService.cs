using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UrlShortener.Models.UserDtos.Registration;

namespace UrlShortener.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UserService(UserManager<IdentityUser> usermanager,
                           SignInManager<IdentityUser> signInManager)
        {
            this.userManager = usermanager;
            this.signInManager = signInManager;
        }
        public async Task<string> CreateUser(RegisterUserDto data)
        {
            //var currentUser = new IdentityUser
            //{
            //    UserName = data.Email,
            //    Email = data.Email
            //};

            //var newUser = await userManager.CreateAsync(currentUser, data.Password);

            //if (newUser.Succeeded)
            //{
            //    await signInManager.SignInAsync(currentUser, isPersistent: false);
            //}
            return "TEST";
        }

        public async Task<string> LoginUser(RegisterUserDto data)
        {
            var user = await userManager.FindByEmailAsync(data.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, data.Password))
            {
              var result = await signInManager.PasswordSignInAsync(user.UserName, data.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return "https://www.google.com";
                    
                }

            }

            return "https://www.abv.bg";
        }
    }
}
