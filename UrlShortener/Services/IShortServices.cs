using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortServices
    {
        public UrlData GetNewUrl (string data);
        public void CreateUrlRecord(UrlData data);
    }
}
