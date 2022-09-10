using System;
using System.Linq;
using System.Threading.Tasks;
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

        public string GetOriginalUrl(string data)
        {
            UrlData result = this.db.UrlDatas.FirstOrDefault(x => x.ShortUrl == data);
            string originalUrl = result.OriginalUrl;

            return originalUrl;
        }

        public Task CreateUrlRecord(UrlData data)
        {
            db.UrlDatas.AddAsync(data);
            db.SaveChangesAsync();

            return Task.CompletedTask;
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
