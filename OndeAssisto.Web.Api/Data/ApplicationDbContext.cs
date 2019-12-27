using Microsoft.EntityFrameworkCore;
using OndeAssisto.Common.Models;

namespace OndeAssisto.Web.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}