using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortService
    {
        public string GetOriginalUrl(string data);
        public UrlData CreateUrlRecord(UrlData data);

        public ExistingUrlRecord isCreated(string originalUrl);

    }
}
