using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using AngleSharp.Dom;
using System.Collections.Generic;
using WorkAndHolidayScraper.Models;

namespace WorkAndHolidayScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScraperController : Controller
    {
        private readonly ILogger logger;
        private readonly Scraper scraper;
        public string url = "https://www.workingholidayjobs.com.au/jobs/";

        public ScraperController(ILogger<ScraperController> logger,
                Scraper scraper)
        {
            this.logger = logger;
            this.scraper = scraper;
        }

        public async Task<ActionResult> Index()
        {
            List<JobRowEntry> jobRowEntries = new List<JobRowEntry>();
            var startTime = DateTime.Now;

            string result ="";
            string nextLink = url;
            do
            {
                IDocument document = await scraper.GetDataPage(nextLink);
                if (scraper.DocumentIsEmpty(document)) nextLink = null;
                else
                {
                    result = scraper.ExtractDataFromDocument(document, jobRowEntries);
                    nextLink = scraper.getNextLink(document);
                }
            }
            while (nextLink != null);

            var totalTime = DateTime.Now - startTime;
            var message = $"You are done! Time spent: {totalTime.Duration()})";
            if (result == "Succeed")
            {
                logger.LogInformation(message);
                return Ok(new
                {
                    Message = message,
                    Results = jobRowEntries
                });
            }
            return Ok(message);
        }

    }
}
