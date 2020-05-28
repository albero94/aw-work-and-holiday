using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class JoraScraper : Scraper
    {
        public JoraScraper(IRepository repository, ILogger<JoraScraper> logger) :
            base(repository, logger, "https://au.jora.com/j?q=working+holiday+visa&l=&sp=homepage")
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelectorAll("article").Count() == 0;

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".result");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = "Jora" };

                try
                {
                    entry.Title = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Title;
                    entry.Href = ((IHtmlAnchorElement)jobRow.QuerySelector("a")).Href;
                    entry.Company = jobRow.QuerySelector(".company") .InnerHtml;
                    //entry.Location = ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobLocation']")).Text +
                    //        ((IHtmlAnchorElement)jobRow.QuerySelector("[data-automation='jobArea']"))?.Text;
                    //entry.Description = ((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobShortDescription']")).TextContent;
                    //entry.Date = DateConversion.DaysHoursAgoStringToDate
                    //        (((IHtmlSpanElement)jobRow.QuerySelector("[data-automation='jobListingDate']")).TextContent);

                    //if (IsValidEntry(entry)) jobRowEntries.Add(entry);
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
            ((IHtmlAnchorElement)document.QuerySelector("[data-automation='page-next']"))?.Href;
    }
}
