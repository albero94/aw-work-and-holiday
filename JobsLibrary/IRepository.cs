using System.Collections.Generic;

namespace JobsLibrary
{
    public interface IRepository
    {
        Job AddJob(Job job);
        IEnumerable<Job> AddJobsFromList(List<Job> jobs);
        IEnumerable<Job> GetJobs(int startIndex, int entriesPerPage);
        IEnumerable<Job> GetFilteredJobs(string searchString, int startIndex, int entriesPerPage);
        int GetJobsNumberForQuery(string searchString);
    }
}
