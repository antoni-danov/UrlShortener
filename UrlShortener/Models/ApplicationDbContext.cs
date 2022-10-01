using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }
        public DbSet<UrlData> UrlDatas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
