using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public Job EditJob(Job jobChanges)
        {
            var job = context.Jobs.Attach(jobChanges);
            job.State = EntityState.Modified;
            context.SaveChanges();
            return jobChanges;
        }

        public Job DeleteJob(Guid Id)
        {
            var job = context.Jobs.Where(j => j.Id == Id).FirstOrDefault();
            if (job == null) return null;

            context.Jobs.Remove(job);
            context.SaveChanges();
            return job;
        }

        public IEnumerable<Job> GetFilteredJobs(string searchString, int startIndex, int entriesPerPage)
        {
            return context.Jobs.Where(j => j.Title.ToLower().Contains(searchString.ToLower()))
                .OrderByDescending(job => job.Date)
                .Skip(startIndex)
                .Take(entriesPerPage);
        }

        public Job GetJob(Guid Id)
        {
            return context.Jobs.Where(j => j.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage)
        {
            return context.Jobs.OrderByDescending(job => job.Date)
                .Skip(startIndex)
                .Take(entriesPerPage);
        }

        public int GetJobsNumberForQuery(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return context.Jobs.Count();
            else
                return context.Jobs.Where(j => j.Title.ToLower().Contains(searchString.ToLower())).Count();
        }

        public IEnumerable<Job> GetUserJobs(IdentityUser user)
        {
            return context.Jobs.Where(j => j.User == user);
        }

        public IEnumerable<JobCategory> GetJobCategories()
        {
            return context.JobCategories;
        }
    }
}
