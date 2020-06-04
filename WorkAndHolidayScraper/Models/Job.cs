using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public string Href { get; set; }
        public string? OriginalWebsite { get; set; }
        public string? Salary { get; set; }
    }
}
