using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    interface IScraper
    {
        public Task<List<Job>> Run();
    }
}
