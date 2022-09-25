using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(int id);
        public List<UrlData> GetAll();
        public Task<User> CreateUser(User data);
        public Task DeleteUrl(int id);

    }
}
