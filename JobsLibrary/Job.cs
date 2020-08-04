using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsLibrary
{
    [Table("job")]
    public class Job
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("title")]
        [Required]
        public string Title { get; set; }
        [Column("company")]
        [Required]
        public string? Company { get; set; }
        [Column("location")]
        [Required]
        public string Location { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("description")]
        [Required]
        public string Description { get; set; }
        [Column("href")]
        public string? Href { get; set; }
        [Column("original_website")]
        public string? OriginalWebsite { get; set; }
        [Column("salary")]
        public string? Salary { get; set; }
    }
}
