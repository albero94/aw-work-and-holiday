using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScraperController : Controller
    {
        public string url = "https://www.workingholidayjobs.com.au/jobs/";
        public ActionResult Index()
        {
            var startTime = DateTime.Now;

            Thread.Sleep(1000);

            var totalTime = DateTime.Now - startTime;
            return Ok($"You are done! Time spent: {totalTime.Duration()})");
        }
    }
}
