using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortServices
    {
        public void CreateUrlRecord(UrlData data);
    }
}
