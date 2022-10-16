using System.Linq;
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

        public ShortService()
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
                Uid = data.Uid
            };

            db.UrlDatas.AddAsync(url);
            db.SaveChangesAsync();

            return url;
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
