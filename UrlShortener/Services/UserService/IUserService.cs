using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public Task<string> CreateUser(RegisterUserDto data);
        public Task<string> LoginUser(RegisterUserDto data);

    }
}
