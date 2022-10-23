using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortService : IShortService
    {
        private ApplicationDbContext db;

        public ShortService(ApplicationDbContext db)
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
        public string GetOriginalUrl(string data)
        {
            UrlData result = db.UrlDatas.FirstOrDefault(x => x.ShortUrl == data);

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
                Uid = data.Uid
            };

            db.UrlDatas.AddAsync(url);
            db.SaveChangesAsync();

            return url;
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
        public ExistingUrlRecord isCreated(string originalUrl)
        {
            var result = this.db.UrlDatas.FirstOrDefault(x => x.OriginalUrl == originalUrl);

            if (result != null)
            {
                var existingUrl = new ExistingUrlRecord()
                {
                    OriginalUrl = result.OriginalUrl,
                    ShortUrl = result.ShortUrl,
                    Uid = result.Uid
                };

                return existingUrl;
            }

            return null;
        }

    }
}
