﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public interface IRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job AddJob(Job job);
        IEnumerable<Job> AddJobsFromList(List<Job> jobs);
    }
}