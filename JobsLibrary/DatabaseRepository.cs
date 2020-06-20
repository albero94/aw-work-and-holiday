using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobsLibrary
{
    public class DatabaseRepository : IRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<DatabaseRepository> logger;

        public DatabaseRepository(AppDbContext context,
            ILogger<DatabaseRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Job AddJob(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
            return job;
        }

        public IEnumerable<Job> AddJobsFromList(List<Job> jobs)
        {
            foreach (var job in jobs)
            {
                context.Jobs.Add(job);
            }
            context.SaveChanges();
            return jobs;
        }

        public IEnumerable<Job> GetFilteredJobs(string serachString, int startIndex, int entriesPerPage)
        {
            return context.Jobs.Where(j => j.Title.Contains(serachString))
                .OrderByDescending(job => job.Date)
                .Skip(startIndex)
                .Take(entriesPerPage);
        }

        public IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage)
        {
            return context.Jobs.OrderByDescending(job => job.Date)
                .Skip(startIndex)
                .Take(entriesPerPage);
        }
    }
}
