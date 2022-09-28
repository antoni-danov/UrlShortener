﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<UrlData> GetAll()
        {
            var result = this.db.UrlDatas.ToList();

            return result;
        }
        public UrlData GetUrlById(int id)
        {
            var url = db.UrlDatas.Where(x => x.UrlId == id).FirstOrDefault();

            return url;
        }
        public User CreateUser(string data)
        {
            var user = new User()
            {
                Email = data
            };

           db.Users.AddAsync(user);
           db.SaveChangesAsync();

           return user;
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

        
    }
}
