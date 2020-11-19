using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Collections.Generic;

namespace JobsLibrary
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity => entity.ToTable(name: "user"));
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "role");
                entity.HasData(new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "Company",
                        NormalizedName = "COMPANY"
                    },
                    new IdentityRole
                    {
                        Name = "User",
                        NormalizedName = "USER"
                    },
                });
            });
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("user_role"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("user_claim"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("user_login"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("role_claim"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("user_token"));
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Directory.GetCurrentDirectory() + "/../ThePopularJob/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("PostgresRemoteDatabase");
            builder.UseNpgsql(connectionString);
            //builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
