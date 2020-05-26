﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace WorkAndHolidayScraper.Models
{
    public class WorkingHolidayJobsScraper : IScraper
    {
        private readonly string mainUrl = "https://www.workingholidayjobs.com.au/jobs/";
        private readonly ILogger<WorkingHolidayJobsScraper> logger;
        private readonly IRepository repository;
        private readonly List<Job> jobRowEntries = new List<Job>();
        
        public WorkingHolidayJobsScraper(ILogger<WorkingHolidayJobsScraper> logger,
                IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<List<Job>> Run()
        {
            logger.LogTrace("Scraper started.");

            string nextLink = mainUrl;
            do
            {
                IDocument document = await GetPageDocument(nextLink);
                if (DocumentIsEmpty(document)) nextLink = null;
                else
                {
                    ExtractDataFromDocument(document, jobRowEntries);
                    nextLink = getNextLink(document);
                }
            }
            while (nextLink != null);

            repository.AddJobsFromList(jobRowEntries);

            logger.LogTrace("Scraper ended.");
            return jobRowEntries;
        }
        private string getNextLink(IDocument document)
        {
            var url = document.Url;

            if (!url.Contains("page")) url += "page/1";
            var currentPage = Regex.Match(url, @"\d+").Value;
            
            if (currentPage == "") return null;
            return url.Replace
                (currentPage, (int.Parse(currentPage) + 1).ToString());
        }
        private async Task<IDocument> GetPageDocument(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);

            logger.LogTrace("Document downloaded");
            return document;
        }
        private string ExtractDataFromDocument(IDocument document, List<Job> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".wpjb-grid-row");
            foreach (var jobRow in rows)
            {
                Job entry = new Job() {OriginalWebsite = "WorkingHolidayJobs" };

                // this returns array with each column document.querySelector(".wpjb-grid-row").innerText.split('\n')
                // but too many \n, worked better in the browser

                entry.Title = jobRow.Children[1].Children[0].Children[0].InnerHtml;
                entry.Href = ((IHtmlAnchorElement)jobRow.Children[1].Children[0].Children[0]).Href;
                entry.Company = jobRow.Children[1].Children[1].InnerHtml;
                entry.Location = jobRow.Children[2].Children[0].Children[0].InnerHtml;
                entry.Type = jobRow.Children[2].Children[1].InnerHtml.Trim();
                entry.Date = jobRow.Children[3].Children[0].InnerHtml.Trim();

                if (!string.IsNullOrEmpty(entry.Title)) jobRowEntries.Add(entry);
            }
            logger.LogTrace("Data extracted from document.");
            return "Succeed";
        }

        internal bool DocumentIsEmpty(IDocument document)
        {
            if (document.QuerySelector(".wpjb-grid-row").TextContent.Contains("No job listings found")) return true;
            else return false;
        }
    }
}