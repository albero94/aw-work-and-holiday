using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkAndHolidayScraper.Models
{
    public interface IScraper
    {
        public Task<List<Job>> Run();
    }
}
