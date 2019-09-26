using backend.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Country> Countries { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                " Server = (localdb)\\MSSQLLocalDB;" +
                " Database = UnderseaDB;" +
                " Trusted_Connection = True;" +
                " MultipleActiveResultSets = true;"); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Country>().ToTable("Countries");

            builder.Entity<Country>().HasKey(c => c.Id);
            builder.Entity<Country>().Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Entity<Country>().Property(c => c.CountryName).HasMaxLength(50);
            builder.Entity<Country>().Property(c => c.Inhabitant).HasDefaultValue(100);
            builder.Entity<Country>().Property(c => c.Pearl).HasDefaultValue(500);
            builder.Entity<Country>().Property(c => c.FlowController).HasDefaultValue(0);
            builder.Entity<Country>().Property(c => c.ReefCastle).HasDefaultValue(0);
            builder.Entity<Country>().Property(c => c.AssaultSeaDog).HasDefaultValue(0);
            builder.Entity<Country>().Property(c => c.BattleSeahorse).HasDefaultValue(0);
            builder.Entity<Country>().Property(c => c.LaserShark).HasDefaultValue(0);
            builder.Entity<Country>().Property(c => c.Points).HasDefaultValue(100);
            builder.Entity<Country>().Property(c => c.Rounds).HasDefaultValue(0);

            builder.Entity<Country>().Property(c => c.MudTractor).HasDefaultValue(false);
            builder.Entity<Country>().Property(c => c.Sludgeharvester).HasDefaultValue(false);
            builder.Entity<Country>().Property(c => c.CoralWall).HasDefaultValue(false);
            builder.Entity<Country>().Property(c => c.SonarGun).HasDefaultValue(false);
            builder.Entity<Country>().Property(c => c.UnderwaterMaterialArts).HasDefaultValue(false);
            builder.Entity<Country>().Property(c => c.Alchemy).HasDefaultValue(false);


            builder.Entity<User>().HasOne(u => u.Country).WithOne(c => c.User).HasForeignKey<Country>(c => c.UserId);

        }


    }
}
