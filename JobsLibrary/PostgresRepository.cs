using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace JobsLibrary
{
    public class PostgresRepository : IRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<PostgresRepository> logger;

        public PostgresRepository(AppDbContext context,
            ILogger<PostgresRepository> logger)
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

        public IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage)
        {
            return context.Jobs.OrderByDescending(job => job.Date)
                .Skip(startIndex)
                .Take(entriesPerPage);
        }
    }
}
