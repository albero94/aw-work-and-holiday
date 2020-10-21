
using JobsLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThePopularJob.ViewModels
{
    public class JobSummaryViewModel
    {
        public string TimeAgo { get; set; }
        public JobCategory SelectedCategory { get; set; }
        public Job Job { get; set; }
    }
}
