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

        public UrlData GetNewUrl(string data) {
           return this.db.UrlDatas.FirstOrDefault(x => x.ShortUrl == data);
        }

        public void CreateUrlRecord(UrlData data)
        {
            var newData = new UrlData
            {
                OriginalUrl = data.OriginalUrl,
                ShortUrl = data.ShortUrl,
                CreatedOn = data.CreatedOn
            };

            db.UrlDatas.Add(newData);
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
