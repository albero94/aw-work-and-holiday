using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsLibrary;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public abstract class Scraper
    {
        protected readonly string mainUrl;
        protected readonly ILogger<Scraper> logger;
        protected readonly IRepository repository;
        protected readonly List<Job> jobRowEntries = new List<Job>();

        public Scraper(IRepository repository,
            ILogger<Scraper> logger,
            string mainUrl)
        {
            this.repository = repository;
            this.logger = logger;
            this.mainUrl = mainUrl;
        }

        public async Task<List<Job>> Run()
        {
            logger.LogTrace("Scraper started.");

            string? nextLink = mainUrl;
            do
            {
                IDocument document = await GetPageDocument(nextLink);
                if (DocumentIsEmpty(document)) nextLink = null;
                else
                {
                    ExtractDataFromDocument(document, jobRowEntries);
                    nextLink = getNextLinkUrl(document);
                }
            }
            while (nextLink != null);

            repository.AddJobsFromList(jobRowEntries);

            logger.LogTrace("Scraper ended.");
            return jobRowEntries;
        }
        private Task<IDocument> GetPageDocument(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = context.OpenAsync(url);
            logger.LogTrace("Document download started");
            return document;
        }

        protected abstract string? getNextLinkUrl(IDocument document);

        protected abstract void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries);

        protected abstract bool DocumentIsEmpty(IDocument document);

        protected bool IsValidEntry(Job entry) =>
            !string.IsNullOrEmpty(entry.Title) && !string.IsNullOrEmpty(entry.Href);
    }
}
