using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace JobsLibrary
{
    public class MockRepository : IRepository
    {
        List<Job> jobList;
        private readonly ILogger logger;

        public MockRepository(ILogger<MockRepository> logger)
        {
            jobList = new List<Job>()
            {
                new Job(){Title = "Test Job", Company ="Test Company", Date = DateTime.Today, Href="www.indeed.com", Location="Australia"},
                new Job(){Title = "Test Job 2", Company ="Test Company 2", Date = DateTime.Today, Href="www.indeed.com", Location="Australia"},
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

        public IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Job> GetFilteredJobs(string serachString, int startIndex, int entriesPerPage)
        {
            throw new NotImplementedException();
        }

        public int GetJobsNumberForQuery(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
