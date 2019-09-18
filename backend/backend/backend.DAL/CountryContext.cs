using backend.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Country> countries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            const string databaseName = "undersea";
            const string trusted = "True";
            const string multiple = "true";

            return $"Server=localhost;" +
                   $"database={databaseName};" +
                   $"Trusted_Connection = {trusted};" +
                   $" MultipleActiveResultSets = {multiple}";
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //táblák konfigurálása-> több-több kapcsolat     
            base.OnModelCreating(builder);

            builder.Entity<User>()
               .HasOne(u => u.country)
               .WithOne()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
