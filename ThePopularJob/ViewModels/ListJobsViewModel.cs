using JobsLibrary;
using System.Collections.Generic;

namespace ThePopularJob.ViewModels
{
    public class ListJobsViewModel
    {
        public ListJobsViewModel()
        {
            Jobs = new List<JobSummaryViewModel>();
        }
        public List<JobSummaryViewModel> Jobs { get; set; }

        public string SearchString { get; set; }
        public int StartIndex { get; set; }
        public int JobsPerPage { get; set; }
        public int JobsNumberForQuery { get; set; }
        public string PageNumber { get; set; }
    }
}



