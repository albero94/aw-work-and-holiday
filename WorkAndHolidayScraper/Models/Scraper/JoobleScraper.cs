using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class JoobleScraper : Scraper
    {
        private readonly string WebsiteName = "Jooble";
        private static readonly string url = "https://au.jooble.org/jobs-working-holiday-visa?p=1";
        public JoobleScraper(IRepository repository, ILogger<JoobleScraper> logger) :
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
                    entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Text;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Href;
                    entry.Company = ((IHtmlElement)jobRow.QuerySelector(".company_region"))?.TextContent;
                    entry.Location = ((IHtmlElement)jobRow.QuerySelector(".date_add-location__region"))?.TextContent;
                    entry.Description = ((IHtmlElement)jobRow.QuerySelector(".desc")).TextContent.Replace('\n', ' ');
                    entry.Date = DateConversion.DaysHoursAgoStringToDate(
                        ((IHtmlElement)jobRow.QuerySelector(".date_add-location__date"))?.TextContent);

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

        protected override string? getNextLinkUrl(IDocument document)
        {
            string pageNumber = new Regex(@"\d").Match(document.BaseUri).ToString();
            string newUrl = document.BaseUri.Split(pageNumber)[0] + (int.Parse(pageNumber) + 1);

            if (NextLinkExists(document, newUrl)) return newUrl;
            else return null;
        }

        private bool NextLinkExists(IDocument document, string newUrl)
        {
            return document.QuerySelectorAll(".paging a").Any(aElement =>
            {
                return ((IHtmlAnchorElement)aElement).Href == newUrl;
            });
        }
    }


}
