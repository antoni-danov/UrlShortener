using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortService
    {
        public Task<IEnumerable<UrlData>> GetAllAsync(string uid);
        public Task<UrlData> GetUrlById(int id);
        public string GetOriginalUrl(string data);
        public UrlData CreateUrlRecord(UrlData data);
        public void DeleteUrl(int id);
        public ExistingUrlRecord isCreated(string originalUrl);

    }
}
