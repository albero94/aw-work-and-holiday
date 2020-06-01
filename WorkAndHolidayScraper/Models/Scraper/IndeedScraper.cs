﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models.Scraper
{
    public class IndeedScraper : Scraper
    {
        private static readonly string url = "https://au.indeed.com/jobs?q=working+holiday+visa&l=";
        public IndeedScraper(IRepository repository, ILogger<IndeedScraper> logger) :
            base(repository, logger, url)
        {
        }

        protected override bool DocumentIsEmpty(IDocument document)
        {
            var result = document.QuerySelectorAll(".result").Count() == 0;
            if(result == true)
            {
                return result;
            }
            return result;
        }
            

        protected override void ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".result");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() { OriginalWebsite = "Indeed" };

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
            var result = ((IHtmlAnchorElement)document.QuerySelector(".pagination-list [aria-label='Next']"))?.Href;
            if (result == null)
            {
                return result;
            }
            return result;
        }
    }
}

