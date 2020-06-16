using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using JobsLibrary;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class SeekScraper : Scraper
    {
        private readonly string WebsiteName = "Seek";
        private static readonly string url = "https://www.seek.com.au/working-holiday-visa-jobs";
        public SeekScraper(IRepository repository, ILogger<SeekScraper> logger) :
            base(repository, logger, url)
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelectorAll("article").Count() == 0;

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll("article");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = WebsiteName };

                try
                {
                    entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobTitle']")).Text;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobTitle']")).Href;
                    entry.Company = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobCompany']"))?.Text;
                    entry.Location = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobLocation']")).Text +
                            ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobArea']"))?.Text;
                    entry.Description = ((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobShortDescription']")).TextContent;
                    entry.Date = DateConversion.TimeAgoStringToDate
                            (((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobListingDate']")).TextContent);

                    if (IsValidEntry(entry)) jobRowEntries.Add(entry);
                }catch (Exception ex)
                {
                    logger.LogWarning($"Job could not be added: {ex.Message}", entry);
                }
            }
            logger.LogTrace("Data extracted from document.");
            return;
        }

        protected override string? getNextLinkUrl(IDocument document) =>
            ((IHtmlAnchorElement)document.QuerySelector("[data-automation='page-next']"))?.Href;
    }
}
