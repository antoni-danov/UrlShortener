using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortServices
    {
        public string GetOriginalUrl(string data);
        public Task CreateUrlRecord(UrlData data);

        public ExistingUrlRecord isCreated(string originalUrl);
    }
}
