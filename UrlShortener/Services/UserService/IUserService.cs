using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public Task<User> CreateUser(User data);
        public User isCreated(string originalUrl);

    }
}
