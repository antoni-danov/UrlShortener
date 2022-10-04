using System;
using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(int id);
        public List<UrlData> GetAll(string uid);
        public void CreateUser(User data);
        public void DeleteUrl(int id);
        public User isCreated(string originalUrl);

    }
}
