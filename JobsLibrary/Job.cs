using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        //[Required]
        public string Location { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("state")]
        public string State { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("description")]
        [Required]
        public string Description { get; set; }
        [Column("long_description")]
        public string LongDescription { get; set; }
        [Column("href")]
        public string? Href { get; set; }
        [Column("original_website")]
        public string? OriginalWebsite { get; set; }
        [Column("salary")]
        public string? Salary { get; set; }
        public JobCategory Category { get; set; }
        public IdentityUser? User { get; set; }
    }
}
