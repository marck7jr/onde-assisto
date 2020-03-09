using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Models;
using OndeAssisto.Common.Models.Jwt;

namespace OndeAssisto.Web.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<MediaPlatform> MediaPlatforms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<JwtTokenRefreshData> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MediaPlatform>()
                .HasKey(x => new { x.MediaGuid, x.PlatformGuid });

            modelBuilder.Entity<MediaPlatform>()
                .HasOne(x => x.Media)
                .WithMany(x => x.Platforms)
                .HasForeignKey(x => x.MediaGuid);

            modelBuilder.Entity<MediaPlatform>()
                .HasOne(x => x.Platform)
                .WithMany(x => x.Medias)
                .HasForeignKey(x => x.PlatformGuid);
                
        }
    }
}