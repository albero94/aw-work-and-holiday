using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WorkAndHolidayScraper.Models;

namespace WorkAndHolidayScraper.Controllers
{
    [ApiController]
    [Route("{controller}/{Action=Index}")]
    public class ScraperController : Controller
    {
        private readonly ILogger logger;
        private readonly IScraper scraper;
        private readonly IRepository repository;

        public ScraperController(ILogger<ScraperController> logger,
                IScraper scraper,
                IRepository repository)
        {
            this.logger = logger;
            this.scraper = scraper;
            this.repository = repository;
        }

        public async Task<ActionResult> Download()
        {
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

        public ActionResult Index()
        {
            logger.LogTrace("Retrieving all jobs in the database");
            return Ok(repository.GetAllJobs());
        }


    }
}
