using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(string id);
        public List<UrlData> GetAll();
        public void DeleteUrl(string id);

    }
}
