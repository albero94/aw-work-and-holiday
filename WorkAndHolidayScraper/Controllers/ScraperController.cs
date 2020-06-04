using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkAndHolidayScraper.Models;
using WorkAndHolidayScraper.Models.Scraper;

namespace WorkAndHolidayScraper.Controllers
{
    [ApiController]
    [Route("{controller}/{Action=Index}/{scraperName?}")]
    public class ScraperController : Controller
    {
        private readonly ILogger logger;
        private readonly IRepository repository;
        private readonly ScraperFactory scraperFactory;

        public ScraperController(ILogger<ScraperController> logger,
                IRepository repository,
                ScraperFactory scraperFactory)
        {
            this.logger = logger;
            this.repository = repository;
            this.scraperFactory = scraperFactory;
        }

        public async Task<ActionResult> Download(string scraperName)
        {
            if (scraperName.ToLower() == "all") return await ScrapeAll();

            var scraper = scraperFactory.GetScraper(scraperName);
            if (scraper == null) return Ok("No data scraped");

            var startTime = DateTime.Now;
            var scrapedData = await scraper.Run();
            var totalTime = DateTime.Now - startTime;

            var message = $"You are done! Time spent: {totalTime.Duration()})";
            logger.LogInformation(message);
            return Ok(new
            {
                Message = message,
                Results = scrapedData
            });
        }

        private async Task<ActionResult> ScrapeAll()
        {
            var startTime = DateTime.Now;

            var tasks = new List<Task>();
            var scraperList = new List<string>() { "Indeed", "Jora", "Jooble", "Seek", "WorkingHolidayJobs" };

            foreach (var scraperName in scraperList)
            {
                Scraper? scraper = scraperFactory.GetScraper(scraperName);
                if (scraper == null) continue;
                tasks.Add(scraper.Run());
            }
            await Task.WhenAll(tasks);

            var totalTime = DateTime.Now - startTime;
            var message = $"You are done! All sites were scraped. Time spent: {totalTime.Duration()})";
            logger.LogInformation(message);

            return Ok(new
            {
                Message = message
            });
        }

        public ActionResult Index()
        {
            logger.LogTrace("Retrieving all jobs in the database");
            //return Ok(repository.GetAllJobs());
            return Ok();
        }


    }
}
