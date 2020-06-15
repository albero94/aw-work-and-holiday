using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JobsLibrary;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class IndeedScraper : Scraper
    {
        private readonly string WebsiteName = "Indeed";
        private readonly int JobNumberLimit = 210;
        private static readonly string url = "https://au.indeed.com/jobs?q=working+holiday+visa&l=";
        public IndeedScraper(IRepository repository, ILogger<IndeedScraper> logger) :
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
                    entry.Company = ((IHtmlElement)jobRow.QuerySelector(".company")).TextContent.Trim();
                    entry.Location = jobRow.QuerySelector(".location").InnerHtml;
                    entry.Description = ((IHtmlElement)jobRow.QuerySelector(".summary")).TextContent.Trim();
                    entry.Date = DateConversion.DaysHoursAgoStringToDate(jobRow.QuerySelector(".date").InnerHtml);
                    entry.Salary = ((IHtmlElement)jobRow.QuerySelector(".salaryText"))?.TextContent.Trim();

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
            try
            {
                var result = ((IHtmlAnchorElement)document.QuerySelector(".pagination-list [aria-label='Next']"))?.Href;
                if (result == null)
                {
                    var jobNumber = new Regex(@"\d+").Match(document.BaseUri).ToString();
                    if (int.Parse(jobNumber) > JobNumberLimit) return null;

                    result = document.BaseUri.Split(jobNumber)[0] + (int.Parse(jobNumber) + 10).ToString();
                }
                return result;
            }
            catch { return null; }
        }
    }
}

