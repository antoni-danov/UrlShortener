﻿using System.Collections.Generic;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public class UserService : IUserService
    {
        private ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<UrlData> GetAll(string uid)
        {
            var result = this.db.UrlDatas.Where(x => x.Uid == uid).OrderByDescending(x => x.CreatedOn).ToList();

            return result;
        }
        public UrlData GetUrlById(int id)
        {
            var url = db.UrlDatas.Where(x => x.UrlId == id).FirstOrDefault();

            return url;
        }
        public void CreateUser(User data)
        {
            var existing = isCreated(data.Uid);

            if (existing == null)
            {
                var user = new User()
                {
                    Uid = data.Uid
                };

                db.Users.Add(user);
                db.SaveChanges();
            }
           
        }
        public void DeleteUrl(int id)
        {
            var existingUrl = GetUrlById(id);

            if (existingUrl != null)
            {
                db.UrlDatas.Remove(existingUrl);
                db.SaveChanges();

            }
            
        }
        public User isCreated(string originalUrl)
        {
            var existingUser = db.Users.FirstOrDefault(x => x.Uid == originalUrl);

            if (existingUser != null)
            {
                return existingUser;
            }

            return null;
        }
    }
}
