using UrlShortener.Models.UserDtos.Login;
using UrlShortener.Models.UserDtos.Registration;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public Task<AuthResponseDto> CreateUser(RegisterUserDto data);
        public Task<AuthResponseDto> LoginUser(LoginUserDto data);
        public Task Logout();

    }
}
