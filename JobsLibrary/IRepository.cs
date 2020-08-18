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
        IEnumerable<Job> GetFilteredJobs(string searchString, int startIndex, int entriesPerPage);
        int GetJobsNumberForQuery(string searchString);
        IEnumerable<Job> GetUserJobs(IdentityUser user);
    }
}
