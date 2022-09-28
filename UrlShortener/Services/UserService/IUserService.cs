﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(int id);
        public List<UrlData> GetAll();
        public User CreateUser(string data);
        public void DeleteUrl(int id);

    }
}
