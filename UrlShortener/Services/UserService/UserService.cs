using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<UrlData>> GetAllAsync(string uid)
        {
            return await db.UrlDatas.Where(x => x.Uid == uid)
                     .OrderByDescending(x => x.CreatedOn).ToListAsync();
        }
        public UrlData GetUrlById(int id)
        {
            var url = db.UrlDatas.Where(x => x.UrlId == id).FirstOrDefault();

            return url;
        }
        public async Task<User> CreateUser(User data)
        {
            var existing = isCreated(data.Uid);

            if (existing == null)
            {
                var user = new User()
                {
                    Uid = data.Uid
                };

                var newUser = await db.Users.AddAsync(user);
                db.SaveChanges();

                return newUser.Entity;

            }

            return existing;
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
