using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models;

public class ApplicationDbContext: IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
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
    public DbSet<DeletedItem> DeletedItems { get; set; }
}
