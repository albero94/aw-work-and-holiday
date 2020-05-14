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
        public string url = "https://www.workingholidayjobs.com.au/jobs/";

        public ScraperController(ILogger<ScraperController> logger)
        {
            this.logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            List<JobRowEntry> jobRowEntries = new List<JobRowEntry>();
            var startTime = DateTime.Now;


            var document = await GetDataPage(url);
            var result = ExtractDataFromDocument(document, jobRowEntries);


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

        private async Task<IDocument> GetDataPage(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            return document;
        }

        private string ExtractDataFromDocument(IDocument document, List<JobRowEntry> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".wpjb-grid-row");
            foreach (var jobRow in rows)
            {
                JobRowEntry entry = new JobRowEntry();

                entry.Title = jobRow.Children[1].Children[0].Children[0].InnerHtml;
                entry.Company = jobRow.Children[1].Children[1].InnerHtml;
                entry.Location = jobRow.Children[2].Children[0].Children[0].InnerHtml;
                entry.Type = jobRow.Children[2].Children[1].InnerHtml.Trim();
                entry.Date = jobRow.Children[3].Children[0].InnerHtml.Trim();

                if (!string.IsNullOrEmpty(entry.Title)) jobRowEntries.Add(entry);
            }
            return "Succeed";
        }
    }
}
