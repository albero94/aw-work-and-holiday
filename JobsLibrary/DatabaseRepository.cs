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

        public IEnumerable<Job> GetFilteredJobs(int startIndex, int entriesPerPage, string searchString, int categoryId)
        {
            var jobs = context.Jobs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString)) jobs = jobs.Where(j => j.Title.ToLower().Contains(searchString.ToLower()));
            if (categoryId != 0) jobs = jobs.Where(j => j.CategoryId == categoryId);
            
            return jobs.OrderByDescending(job => job.Date)
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

        public int GetJobsCountForQuery(string searchString, int categoryId)
        {
            var jobs = context.Jobs.AsQueryable();
            if (!string.IsNullOrEmpty(searchString)) jobs = jobs.Where(j => j.Title.ToLower().Contains(searchString.ToLower()));
            if (categoryId != 0) jobs = jobs.Where(j => j.CategoryId == categoryId);

            return jobs.Count();
        }

        public IEnumerable<Job> GetUserJobs(ApplicationUser user)
        {
            return context.Jobs.Where(j => j.User == user);
        }

        public IEnumerable<JobCategory> GetJobCategories()
        {
            return context.JobCategories;
        }
    }
}
