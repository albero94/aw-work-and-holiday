using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JobsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThePopularJob.Models;
using ThePopularJob.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ThePopularJob.Controllers
{
    [Authorize]
    public class JobPostings : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRepository repository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly int jobsPerPage = 20;

        public JobPostings(ILogger<HomeController> logger,
                IRepository repository,
                UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.repository = repository;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddJob()
        {
            var jobCategories = repository.GetJobCategories();
            var jobViewModel = new JobViewModel{ Categories = jobCategories, Job = new Job()};
            return View(jobViewModel);
        }

        [HttpPost] 
        public async Task<IActionResult> AddJob(JobViewModel jobViewModel)
        {
            if (!ModelState.IsValid) return View(jobViewModel);

            var job = jobViewModel.Job;
            job.Date = DateTime.Now;
            job.OriginalWebsite = "ThePopularJob";
            job.Href = "#";
            job.User = await userManager.GetUserAsync(User);

            var result = repository.AddJob(job);
            return RedirectToAction("ListJobs");
        }

        [HttpPost]
        public IActionResult DeleteJob(Guid Id)
        {
            repository.DeleteJob(Id);
            return RedirectToAction("ListJobs");
        }

        [HttpGet]
        public IActionResult EditJob(Guid Id)
        {
            var jobCategories = repository.GetJobCategories();
            var job = repository.GetJob(Id);
            var jobViewModel = new JobViewModel
            {
                Job = job,
                Categories = jobCategories
            };
            return View(jobViewModel);
        }

        [HttpPost]
        public async Task<IActionResult>  EditJob(JobViewModel jobViewModel)
        {
            if (!ModelState.IsValid) return View();
            jobViewModel.Job.User = await userManager.GetUserAsync(User);
            var result = repository.EditJob(jobViewModel.Job);
            return RedirectToAction("ListJobs");
        }

        [HttpGet]
        public async Task<IActionResult> ListJobs()
        {
            var user = await userManager.GetUserAsync(User);
            var jobs = repository.GetUserJobs(user);
            return View(jobs);
        }
    }
}
