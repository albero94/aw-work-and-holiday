using JobsLibrary;
using System.Collections.Generic;

namespace ThePopularJob.ViewModels
{
    public class ListJobsViewModel
    {
        public ListJobsViewModel()
        {
            Jobs = new List<JobViewModel>();
        }
        public List<JobViewModel> Jobs { get; set; }

        public string SearchString { get; set; }
        public int StartIndex { get; set; }
        public int JobsPerPage { get; set; }
    }
}



