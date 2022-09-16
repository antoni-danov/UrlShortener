using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public List<UrlData> GetAll();

    }
}
