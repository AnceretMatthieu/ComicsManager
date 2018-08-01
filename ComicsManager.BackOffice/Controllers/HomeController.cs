using ComicsManager.BackOffice.ViewModels.Home;
using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var lastComics = new List<SimpleComicViewModel>();
            var comics  = _context.Comics
                .Include(c => c.Genre)
                .OrderBy(c => c.CreatedOn)
                .ThenBy(c => c.ModifiedOn)
                .Take(_config.Value.LastItemNumberInHomePage);
            foreach(var comic in comics)
            {
                var simpleComic = new SimpleComicViewModel
                {
                    Id = comic.Id,
                    Title = comic.Title,
                    Genre = comic.Genre.Title,
                    CouvertureFileId = comic.CouvertureId
                };

                lastComics.Add(simpleComic);
            }

            ViewBag.LastComics = lastComics;

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
            return View();
        }
    }
}
