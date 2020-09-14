
using JobsLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ThePopularJob.ViewModels
{
    public class JobViewModel
    {
        public Job Job { get; set; }
        public IEnumerable<JobCategory> Categories { get; set; }
    }
}
