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
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> AddJob(Job job)
        {
            if (!ModelState.IsValid) return View();

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
            var job = repository.GetJob(Id);
            return View(job);
        }

        [HttpPost]
        public IActionResult EditJob(Job job)
        {
            if (!ModelState.IsValid) return View();

            var result = repository.EditJob(job);
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
