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
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<JwtTokenRefreshData> Tokens { get; set; }
    }
}