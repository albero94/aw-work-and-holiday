
using JobsLibrary;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThePopularJob.ViewModels
{
    public class JobViewModel
    {
        public string Title { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public string? City { get; set; }
        public string? State{ get; set; }
        public string TimeAgo { get; set; }
        public string? Description { get; set; }
        public string? LongDescription { get; set; }
        public string Href { get; set; }
        public string? Salary { get; set; }
        public JobCategory SelectedCategory { get; set; }
        public IEnumerable<JobCategory> Categories { get; set; }
    }
}
