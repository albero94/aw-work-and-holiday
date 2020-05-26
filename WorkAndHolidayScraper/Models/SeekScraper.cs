using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public class SeekScraper : IScraper
    {
        private readonly string mainUrl = "https://www.seek.com.au/working-holiday-visa-jobs";
        private readonly ILogger<SeekScraper> logger;
        private readonly IRepository repository;
        private readonly List<Job> jobRowEntries = new List<Job>();

        public SeekScraper(ILogger<SeekScraper> logger,
                IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<List<Job>> Run()
        {
            logger.LogTrace("Scraper started.");

            string nextLink = mainUrl;
            do
            {
                IDocument document = await GetPageDocument(nextLink);
                if (DocumentIsEmpty(document)) nextLink = null;
                else
                {
                    ExtractDataFromDocument(document, jobRowEntries);
                    nextLink = getNextLink(document);
                }
            }
            while (nextLink != null);

            repository.AddJobsFromList(jobRowEntries);

            logger.LogTrace("Scraper ended.");
            return jobRowEntries;
        }

        private string getNextLink(IDocument document)
        {
            var url = ((IHtmlAnchorElement)document.QuerySelector("[data-automation='page-next']"))?.Href;
            return url;
        }

        private async Task<IDocument> GetPageDocument(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            logger.LogTrace("Document downloaded");
            return document;
        }

        private string ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll("article");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = "Seek" };

                entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobTitle']"))?.Text;
                entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobTitle']")).Href;
                entry.Company = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobCompany']"))?.Text;
                entry.Location = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobLocation']"))?.Text +
                        ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobArea']"))?.Text;
                entry.Description = ((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobShortDescription']"))?.TextContent;
                entry.Date = DateConversion.DaysHoursAgoStringToDate
                        (((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobListingDate']"))?.TextContent);

                if (!string.IsNullOrEmpty(entry.Title) && !string.IsNullOrEmpty(entry.Href))
                    jobRowEntries.Add(entry);
            }
            logger.LogTrace("Data extracted from document.");
            return "Succeed";
        }

        internal bool DocumentIsEmpty(IDocument document)
        {
            return document.QuerySelectorAll("article").Count() == 0;
        }
    }
}
