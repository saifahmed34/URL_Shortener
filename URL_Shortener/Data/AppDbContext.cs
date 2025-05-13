using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;

namespace URL_Shortener.Data
{
    public class AppDbContext : DbContext
    {
        // Correct constructor - takes DbContextOptions<AppDbContext>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OriginalUrl)
                    .IsRequired()
                    .HasMaxLength(2000);
                entity.Property(e => e.ShortCode)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.HasIndex(e => e.ShortCode)
                    .IsUnique();
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.ClickCount)
                    .HasDefaultValue(0);
            });
        }
    }
}