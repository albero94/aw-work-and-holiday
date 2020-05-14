using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WorkAndHolidayScraper.Models;

namespace WorkAndHolidayScraper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScraperController : Controller
    {
        private readonly ILogger logger;
        private readonly Scraper scraper;

        public ScraperController(ILogger<ScraperController> logger,
                Scraper scraper)
        {
            this.logger = logger;
            this.scraper = scraper;
        }

        public async Task<ActionResult> Index()
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
    }
}
