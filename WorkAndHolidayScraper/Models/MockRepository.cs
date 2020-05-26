using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public class MockRepository : IRepository
    {
        List<Job> jobList;
        private readonly ILogger logger;

        public MockRepository(ILogger<MockRepository> logger)
        {
            jobList = new List<Job>()
            {
                new Job(){Title = "Test Job", Company ="Test Company", Date = DateTime.Today, Href="www.indeed.com", Location="Australia", Type="Full time"},
                new Job(){Title = "Test Job 2", Company ="Test Company 2", Date = DateTime.Today, Href="www.indeed.com", Location="Australia", Type="Part time"},
            };
            this.logger = logger;

        }

        public IEnumerable<Job> AddJobsFromList(List<Job> jobs)
        {
            foreach (var job in jobs)
            {
                jobList.Add(job);
            }

            logger.LogTrace("Jobs added from list.");
            return jobs;
        }

        public Job AddJob(Job job)
        {
            return job;
        }

        public IEnumerable<Job> GetAllJobs()
        {
            return jobList;
        }
    }
}
