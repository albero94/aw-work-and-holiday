using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace WorkAndHolidayScraper.Models
{
    public class Scraper
    {
        private readonly ILogger<Scraper> logger;

        public Scraper(ILogger<Scraper> logger)
        {
            this.logger = logger;
        }

        public string getNextLink(IDocument document)
        {
            var url = document.Url;

            if (!url.Contains("page")) url += "page/1";
            var currentPage = Regex.Match(url, @"\d+").Value;
            
            if (currentPage == "") return null;
            return url.Replace
                (currentPage, (int.Parse(currentPage) + 1).ToString());
        }
        public async Task<IDocument> GetDataPage(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            return document;
        }
        public string ExtractDataFromDocument(IDocument document, List<JobRowEntry> jobRowEntries)
        {
            var rows = document.QuerySelectorAll(".wpjb-grid-row");
            foreach (var jobRow in rows)
            {
                JobRowEntry entry = new JobRowEntry();

                entry.Title = jobRow.Children[1].Children[0].Children[0].InnerHtml;
                // This also gets the title: document.querySelector(".wpjb-grid-row a").innerText
                // this returns array with each column document.querySelector(".wpjb-grid-row").innerText.split('\n')
                entry.Company = jobRow.Children[1].Children[1].InnerHtml;
                entry.Location = jobRow.Children[2].Children[0].Children[0].InnerHtml;
                entry.Type = jobRow.Children[2].Children[1].InnerHtml.Trim();
                entry.Date = jobRow.Children[3].Children[0].InnerHtml.Trim();

                if (!string.IsNullOrEmpty(entry.Title)) jobRowEntries.Add(entry);
            }
            return "Succeed";
        }

        internal bool DocumentIsEmpty(IDocument document)
        {
            if (document.QuerySelector(".wpjb-grid-row").TextContent.Contains("No job listings found")) return true;
            else return false;
        }
    }
}
