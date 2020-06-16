using System.Collections.Generic;

namespace ThePopularJob.ViewModels
{
    public class ListJobsViewModel
    {
        public ListJobsViewModel()
        {
            Jobs = new List<ListJobsViewModel>();
        }
        public List<ListJobsViewModel> Jobs { get; set; }
    }
}



