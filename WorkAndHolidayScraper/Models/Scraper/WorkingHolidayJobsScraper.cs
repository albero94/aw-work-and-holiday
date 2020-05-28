using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Primitives;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class WorkingHolidayJobsScraper : Scraper
    {
        public WorkingHolidayJobsScraper(IRepository repository, ILogger<WorkingHolidayJobsScraper> logger) :
            base(repository, logger, "https://www.workingholidayjobs.com.au/jobs/")
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelectorAll("article").Count() == 0;

        protected override string ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".wpjb-grid-row");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = "WorkingHolidayJobs" };

                // this returns array with each column document.querySelector(".wpjb-grid-row").innerText.split('\n')
                // but too many \n, worked better in the browser
                try
                {

                    entry.Title = jobRow.Children[1].Children[0].Children[0].InnerHtml;
                    entry.Href = ((IHtmlAnchorElement)jobRow.Children[1].Children[0].Children[0]).Href;
                    entry.Company = jobRow.Children[1].Children[1].InnerHtml;
                    entry.Location = jobRow.Children[2].Children[0].Children[0].InnerHtml;
                    entry.Type = jobRow.Children[2].Children[1].InnerHtml.Trim();
                    entry.Date = DateConversion.MonthDayStringToDate(jobRow.Children[3].Children[0].InnerHtml.Trim());

                    if (IsValidEntry(entry)) jobRowEntries.Add(entry);
                }catch(Exception ex)
                {
                    logger.LogWarning("Job could not be added", ex.Message, entry);
                }
            }
            logger.LogTrace("Data extracted from document.");
            return "Succeed";
        }

        protected override string? getNextLinkUrl(IDocument document)
        {
            var url = document.Url;
            if (!url.Contains("page")) url += "page/1";
            var currentPage = Regex.Match(url, @"\d+").Value;

            if (currentPage == "") return null;
            return url.Replace
                (currentPage, (int.Parse(currentPage) + 1).ToString());
        }
    }
}
