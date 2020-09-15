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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRepository repository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly int jobsPerPage = 20;

        public HomeController(ILogger<HomeController> logger,
                IRepository repository,
                UserManager<IdentityUser> userManager)
        {
            this.logger = logger;
            this.repository = repository;
            this.userManager = userManager;
        }

        public IActionResult Index(int startIndex)
        {
            return View();
        }

        public IActionResult ListJobs(string searchString, int startIndex, int selectedCategoryId)
        {
            var jobsCount = repository.GetJobsCountForQuery(searchString, selectedCategoryId);
            var model = new ListJobsViewModel();
            model.StartIndex = startIndex;
            model.JobsPerPage = jobsPerPage;
            model.SearchString = searchString;
            model.JobsNumberForQuery = jobsCount;
            model.PageNumber = $"Page {startIndex / jobsPerPage + 1} of " +
                $"{Math.Ceiling( jobsCount / (float)jobsPerPage)}";
            model.Categories = repository.GetJobCategories();
            
            var jobs = repository.GetFilteredJobs(startIndex, jobsPerPage, searchString, selectedCategoryId);

            foreach (var job in jobs)
            {
                model.Jobs.Add(new JobSummaryViewModel
                {
                    Title = job.Title,
                    TimeAgo = DateConversion.DateTimeToTimeAgo(job.Date),
                    Company = job.Company,
                    Description = job.Description,
                    Href = job.Href,
                    Location = job.Location,
                    Salary = job.Salary
                });
            }

            return View(model);
        }

        public IActionResult JobDetails(Guid Id)
        {
            var job = repository.GetJob(Id);
            return View(job);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
