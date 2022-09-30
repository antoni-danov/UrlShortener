﻿using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public interface IUserService
    {
        public UrlData GetUrlById(int id);
        public List<UrlData> GetAll();
        public User CreateUser(User data);
        public void DeleteUrl(int id);

    }
}
