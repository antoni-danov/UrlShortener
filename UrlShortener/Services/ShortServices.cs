using System;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortServices : IShortServices
    {
        private ApplicationDbContext db;

        public ShortServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ShortServices()
        {
        }

        public UrlData GetNewUrl(string data) {
           return this.db.UrlDatas.FirstOrDefault(x => x.ShortUrl == data);
        }

        public void CreateUrlRecord(UrlData data)
        {
            db.UrlDatas.Add(data);
            db.SaveChanges();

        }

        public bool isCreated(string originalUrl)
        {
            var result = this.db.UrlDatas.FirstOrDefault(x => x.OriginalUrl == originalUrl);

            if(result != null)
            {
                return true;
            }

            return false;
        }
    }
}
