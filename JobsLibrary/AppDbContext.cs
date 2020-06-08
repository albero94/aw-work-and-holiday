using Microsoft.EntityFrameworkCore;
using System;

namespace JobsLibrary
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
}
