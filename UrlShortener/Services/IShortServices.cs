using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortServices
    {
        public UrlData GetNewUrl (string data);
        public string GetOriginalUrl(string data);
        public Task CreateUrlRecord(UrlData data);

        public bool isCreated(string originalUrl);
    }
}
