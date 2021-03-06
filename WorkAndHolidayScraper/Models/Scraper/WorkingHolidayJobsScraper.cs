﻿using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using JobsLibrary;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class WorkingHolidayJobsScraper : Scraper
    {
        private readonly string WebsiteName = "WorkingHolidayJobs";
        private static readonly string url = "https://www.workingholidayjobs.com.au/jobs/";
        public WorkingHolidayJobsScraper(IRepository repository, ILogger<WorkingHolidayJobsScraper> logger) :
            base(repository, logger, url)
        {
        }

        protected override bool DocumentIsEmpty(IDocument document) =>
            document.QuerySelector(".wpjb-grid-row").TextContent.Contains("No job listings found");

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".wpjb-grid-row");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = WebsiteName };
                try
                {
                    entry.Title = jobRow.Children[1].Children[0].Children[0].InnerHtml;
                    entry.Href = ((IHtmlAnchorElement)jobRow.Children[1].Children[0].Children[0]).Href;
                    entry.Company = jobRow.Children[1].Children[1].InnerHtml;
                    entry.Location = jobRow.Children[2].Children[0].Children[0].InnerHtml;
                    entry.Date = DateConversion.MonthDayStringToDate(jobRow.Children[3].Children[0].InnerHtml.Trim());

                    if (IsValidEntry(entry)) jobRowEntries.Add(entry);
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Job could not be added", ex.Message, entry);
                }
            }
            logger.LogTrace("Data extracted from document.");
            return;
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
