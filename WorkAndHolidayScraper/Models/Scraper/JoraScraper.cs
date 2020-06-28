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
    public class JoraScraper : Scraper
    {
        private readonly string WebsiteName = "Jora";
        private static readonly string url = "https://au.jora.com/j?sp=search&q=working+holiday+visa&l=";
        public JoraScraper(IRepository repository, ILogger<JoraScraper> logger) :
            base(repository, logger, url)
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelectorAll(".result").Count() == 0;

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".result");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = WebsiteName };

                try
                {
                    entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Title;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Href;
                    entry.Company = jobRow.QuerySelector(".job-company")?.InnerHtml ?? jobRow.QuerySelector(".company")?.InnerHtml;
                    entry.Location = jobRow.QuerySelector(".job-location")? .InnerHtml ?? jobRow.QuerySelector(".location")?.InnerHtml;
                    entry.Description = jobRow.QuerySelector(".job-abstract")? .InnerHtml ?? jobRow.QuerySelector(".summary")?.InnerHtml;
                    entry.Date = DateConversion.TimeAgoStringToDate(jobRow.QuerySelector(".job-listed-date")? .InnerHtml ?? jobRow.QuerySelector(".date")?.InnerHtml);

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
            ((IHtmlAnchorElement)document.QuerySelector("a.next-page-button"))?.Href ??
            ((IHtmlAnchorElement)document.QuerySelector("a.next_page"))?.Href;
    }
}
