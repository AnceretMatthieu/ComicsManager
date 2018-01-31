using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ComicsManager.BackOffice.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ComicsManagerContext context,
            IOptions<AppSettings> config)
            : base(context, config)
        {

        }

        public IActionResult Index()
        {
            ViewBag.LastComics = _context.Comics
                .OrderBy(c => c.CreatedOn)
                .ThenBy(c => c.ModifiedOn)
                .Take(_config.Value.LastItemNumberInHomePage);

            ViewBag.LastAuthors = _context.Authors
                .OrderBy(c => c.CreatedOn)
                .ThenBy(c => c.ModifiedOn)
                .Take(_config.Value.LastItemNumberInHomePage);

            ViewBag.LastEditors = _context.Editors
                .OrderBy(c => c.CreatedOn)
                .ThenBy(c => c.ModifiedOn)
                .Take(_config.Value.LastItemNumberInHomePage);

            ViewBag.LastGenres = _context.Genres
                .OrderBy(c => c.CreatedOn)
                .ThenBy(c => c.ModifiedOn)
                .Take(_config.Value.LastItemNumberInHomePage);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}
