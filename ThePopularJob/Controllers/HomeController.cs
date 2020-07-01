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

namespace ThePopularJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IRepository repository;
        private readonly int jobsPerPage = 20;

        public HomeController(ILogger<HomeController> logger,
                IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index(int startIndex)
        {
            ViewBag.ShowBanner = true;
            return View();
        }

        public IActionResult ListJobs(string searchString, int startIndex)
        {

            var jobs = string.IsNullOrEmpty(searchString) ?
                repository.GetJobs(startIndex, jobsPerPage) :
                repository.GetFilteredJobs(searchString, startIndex, jobsPerPage);

            var model = new ListJobsViewModel();
            model.StartIndex = startIndex;
            model.JobsPerPage = jobsPerPage;
            model.SearchString = searchString;

            foreach (var job in jobs)
            {
                model.Jobs.Add(new JobViewModel
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

            ViewBag.ShowBanner = true;
            return View(model);
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
