using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(int id);
        public Task<IEnumerable<UrlData>> GetAllAsync(string uid);
        public Task<User> CreateUser(User data);
        public void DeleteUrl(int id);
        public User isCreated(string originalUrl);

    }
}
