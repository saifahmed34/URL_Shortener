using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
