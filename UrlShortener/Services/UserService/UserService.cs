using System.Collections.Generic;
using System.Linq;
using UrlShortener.Models;

namespace UrlShortener.Services.UserService
{
    public class UserService: IUserService
    {
        private ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<UrlData> GetAll()
        {
            var result = this.db.UrlDatas.ToList();

            return result;
        }
    }
}
