﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JobsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ThePopularJob.Models;

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
            ViewBag.StartIndex = startIndex;
            ViewBag.JobEntriesPerPage = jobEntriesPerPage;
            return View(jobs);
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
