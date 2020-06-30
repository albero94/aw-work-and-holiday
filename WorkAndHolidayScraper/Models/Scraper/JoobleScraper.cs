using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using JobsLibrary;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class JoobleScraper : Scraper
    {
        private readonly string WebsiteName = "Jooble";
        private static readonly string url = "https://au.jooble.org/m/jobs-working-holiday-visa?p=99";
        public JoobleScraper(IRepository repository, ILogger<JoobleScraper> logger) :
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
                if (jobRow.QuerySelector("section").Children.Length != 4) continue;
                Job entry = new Job() { OriginalWebsite = WebsiteName };

                try
                {
                    entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Text;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Href.Replace("jooble.org/m/", "jooble.org/");
                    entry.Company = jobRow.QuerySelector("section").Children[0].TextContent;
                    entry.Location = jobRow.QuerySelector("section").Children[2].TextContent;
                    entry.Description = jobRow.QuerySelector("section").Children[1].TextContent.Replace('\n', ' ');
                    entry.Date = DateConversion.TimeAgoStringToDate(
                        jobRow.QuerySelector("section").Children[3].TextContent);

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

        protected override string? getNextLinkUrl(IDocument document) => null;

        protected override bool IsValidEntry(Job entry) =>
            !string.IsNullOrEmpty(entry.Title) &&
            !string.IsNullOrEmpty(entry.Href) &&
            !entry.Title.Contains("China", StringComparison.CurrentCultureIgnoreCase) &&
            (!entry.Location?.Contains("China", StringComparison.CurrentCultureIgnoreCase) ?? true);
    }


}
