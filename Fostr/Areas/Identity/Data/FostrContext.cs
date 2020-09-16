using Fostr.Areas.Identity.Data;
using Fostr.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fostr.Data
{
    public class FostrContext : IdentityDbContext<FostrUser>
    {
        public FostrContext(DbContextOptions<FostrContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=Fostr;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalTutor> AnimalTutor { get; set; }
    }
}
