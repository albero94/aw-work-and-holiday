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
        private readonly int jobEntriesPerPage = 20;

        public HomeController(ILogger<HomeController> logger,
                IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index(int startIndex)
        {
            var jobs = repository.GetJobs(startIndex, jobEntriesPerPage);

            var model = new List<JobViewModel>();

            foreach(var job in jobs)
            {
                model.Add(new JobViewModel
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

            ViewBag.StartIndex = startIndex;
            ViewBag.JobEntriesPerPage = jobEntriesPerPage;
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
