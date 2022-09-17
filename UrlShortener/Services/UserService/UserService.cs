using System;
using System.Collections.Generic;
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

        public List<UrlData> GetAll()
        {
            var result = this.db.UrlDatas.ToList();

            return result;
        }

        public UrlData GetUrlById(string id)
        {
            var guidId = Guid.Parse(id);
            var url = db.UrlDatas.Where(x => x.UrlId == guidId).FirstOrDefault();

            return url;
        }
        public void DeleteUrl(string id)
        {
            var existingUrl = GetUrlById(id);

            db.UrlDatas.Remove(existingUrl);
            db.SaveChanges();
        }

    }
}
