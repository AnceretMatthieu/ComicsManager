using ComicsManager.BackOffice.ViewModels.Home;
using ComicsManager.Common;
using ComicsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
            var lastComics = new List<SimpleComicViewModel>();
            var comics  = _context.Comics
                .Include(c => c.Couverture)
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
                    Genre = comic.Genre.Title
                };

                if (comic.Couverture != null)
                {
                    string b64image = Convert.ToBase64String(comic.Couverture.Path);
                    simpleComic.CouvertureFileB64 = string.Format("data:image/png;base64,{0}", b64image);
                }

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
