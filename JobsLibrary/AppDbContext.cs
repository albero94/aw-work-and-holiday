using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace JobsLibrary
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    Id = Guid.NewGuid(),
                    Title = "Test title",
                    Company = "Test company",
                    Date = DateTime.Now,
                    Description = "Test description",
                    Href = "www.google.com",
                    Location = "Australia",
                    Salary = "a lot :)",
                    OriginalWebsite = "Test",
                }
            );
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../ThePopularJob/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultString");
            builder.UseNpgsql(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
