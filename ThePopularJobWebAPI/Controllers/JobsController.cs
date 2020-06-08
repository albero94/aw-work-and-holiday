using Microsoft.AspNetCore.Mvc;
using JobsLibrary;

namespace ThePopularJobWebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class JobsController : Controller
    {
        private readonly IRepository repository;
        private readonly int entriesPerPage = 20;

        public JobsController(IRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index(int startIndex = 0)
        {
            var jobList = repository.GetJobs(startIndex, entriesPerPage);
            ViewBag.StartIndex = startIndex;
            ViewBag.EntriesPerPage = entriesPerPage;
            return View(jobList);
        }
    }
}
