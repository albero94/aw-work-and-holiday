using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class ScraperFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ScraperFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Scraper? GetScraper(string name)
        {
            return name.ToLower() switch
            {
                "indeed" => (Scraper)serviceProvider.GetService(typeof(IndeedScraper)),
                "jora" => (Scraper)serviceProvider.GetService(typeof(JoraScraper)),
                "seek" => (Scraper)serviceProvider.GetService(typeof(SeekScraper)),
                "jooble" => (Scraper)serviceProvider.GetService(typeof(JoobleScraper)),
                "workingholidayjobs" => (Scraper)serviceProvider.GetService(typeof(WorkingHolidayJobsScraper)),
                "backpackerjobboard" => (Scraper)serviceProvider.GetService(typeof(BackpackerJobBoardScraper)),
                _ => null
            };
        }
    }
}
