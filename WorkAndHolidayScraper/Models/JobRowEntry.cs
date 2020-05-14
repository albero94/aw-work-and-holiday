using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public class JobRowEntry
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
    }
}
