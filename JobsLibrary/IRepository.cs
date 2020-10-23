using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobsLibrary
{
    public interface IRepository
    {
        Job AddJob(Job job);
        IEnumerable<Job> AddJobsFromList(List<Job> jobs);
        Job DeleteJob(Guid Id);
        Job EditJob(Job jobChanges);
        Job GetJob(Guid Id);
        IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage);
        IEnumerable<Job> GetFilteredJobs(int startIndex, int entriesPerPage, string searchString, int categoryId);
        int GetJobsCountForQuery(string searchString, int categoryId);
        IEnumerable<Job> GetUserJobs(ApplicationUser user);
        IEnumerable<JobCategory> GetJobCategories();
    }
}
