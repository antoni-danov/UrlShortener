using Microsoft.AspNetCore.Mvc;
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
        public string GetOriginalUrl(string data)
        {
            UrlData result = this.db.UrlDatas.FirstOrDefault(x => x.ShortUrl == data);

            if (result != null)
            {
                string originalUrl = result.OriginalUrl;
                return originalUrl;

            }

            return null;
        }

        public UrlData CreateUrlRecord(UrlData data)
        {
            var url = new UrlData
            {
                OriginalUrl = data.OriginalUrl,
                ShortUrl = data.ShortUrl,
                CreatedOn = data.CreatedOn,
            };

            db.UrlDatas.Add(data);
            db.SaveChanges();

            return data;
        }

        public ExistingUrlRecord isCreated(string originalUrl)
        {
            var result = this.db.UrlDatas.FirstOrDefault(x => x.OriginalUrl == originalUrl);

            if (result != null)
            {
                var existingUrl = new ExistingUrlRecord()
                {
                    OriginalUrl = result.OriginalUrl,
                    ShortUrl = result.ShortUrl
                };

                return existingUrl;
            }

            return null;
        }

    }
}
