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
    public class BackpackerJobBoardScraper : Scraper
    {
        private readonly string WebsiteName = "BackpackerJobBoard";
        private static readonly string url = "https://www.backpackerjobboard.com.au/jobs/au-pair-jobs/";
        public BackpackerJobBoardScraper(IRepository repository, ILogger<SeekScraper> logger) :
            base(repository, logger, url)
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelectorAll(".job-entry").Count() == 0;

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".job-entry");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = WebsiteName };

                try
                {
                    entry.Title = ((IHtmlElement)jobRow.QuerySelector(".job-title")).TextContent;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector(".job-title a")).Href;
                    entry.Company = ((IHtmlElement)jobRow.QuerySelector(".job-company")).TextContent;
                    entry.Location = ((IHtmlElement)jobRow.QuerySelector(".job-location")).TextContent;
                    entry.Description = ((IHtmlElement)jobRow.QuerySelector(".job-excerpt"))?.TextContent;
                    entry.Date = DateConversion.DayMonthYearToDate
                            (((IHtmlElement)jobRow.QuerySelector(".job-datestamp"))?.TextContent);

                    if (IsValidEntry(entry)) jobRowEntries.Add(entry);
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Job could not be added: {ex.Message}", entry);
                }
            }
            logger.LogTrace("Data extracted from document.");
            return;
        }

        protected override string? getNextLinkUrl(IDocument document) =>
            ((IHtmlAnchorElement)
            document.QuerySelectorAll("[rel='noindex,follow']").Last())
            .Href;
    }
}
