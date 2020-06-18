using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsLibrary
{
    [Table("Job")]
    public class Job
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string Href { get; set; }
        public string? OriginalWebsite { get; set; }
        public string? Salary { get; set; }
    }
}
