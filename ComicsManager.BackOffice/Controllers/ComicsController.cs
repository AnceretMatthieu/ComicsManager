using ComicsManager.BackOffice.ViewModels;
using ComicsManager.Common;
using ComicsManager.Model;
using ComicsManager.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsManager.BackOffice.Controllers
{
    public class ComicsController : BaseController
    { 
        public ComicsController(
            ComicsManagerContext context,
            IOptions<AppSettings> config)
            : base(context, config)
        {
            
        }

        // GET: Comics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comics.ToListAsync());
        }

        // GET: Comics/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics
                .Include(c => c.Couverture)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comic == null)
            {
                return NotFound();
            }

            var vm = new ComicViewModel
            {
                Comic = comic
            };

            if (comic.Couverture != null)
            {
                string b64image = Convert.ToBase64String(comic.Couverture.Path);
                vm.CouvertureFileB64 = string.Format("data:image/png;base64,{0}", b64image);
            }

            return View(vm);
        }

        // GET: Comics/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Editors = _context.Editors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> couvertureFile, [Bind("Title,ISBN,Cycle,Collection,Note,PublicationDate,Id,CreatedOn,ModifiedOn,GenreId")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                // Gestion des fichiers uploadés via le Drag'n'Drop
                if (TempData.ContainsKey(Constants.UploadedFilesTempDataKey))
                {
                    var filesId = (List<Guid>)TempData[Constants.UploadedFilesTempDataKey];
                    comic.FileId = filesId.FirstOrDefault();
                }

                // Upload du fichier de la couverture
                if(couvertureFile.Count != 0)
                {
                    var file = couvertureFile.FirstOrDefault();

                    var fileEntity = new Model.Models.File
                    {
                        Type = Path.GetExtension(file.ContentDisposition)
                    };

                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        fileEntity.Path = memoryStream.ToArray();
                    }

                    _context.Files.Add(fileEntity);
                    await _context.SaveChangesAsync();

                    comic.CouvertureId = fileEntity.Id;
                }

                _context.Add(comic);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Editors = _context.Editors.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(comic);
        }

        // GET: Comics/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics.SingleOrDefaultAsync(m => m.Id == id);
            if (comic == null)
            {
                return NotFound();
            }
            return View(comic);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,ISBN,Cycle,Collection,Note,Couverture,PublicationDate,Id,CreatedOn,ModifiedOn,GenreId")] Comic comic)
        {
            if (id != comic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComicExists(comic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var comic = await _context.Comics.SingleOrDefaultAsync(m => m.Id == id);
            if(comic == null)
            {
                return NotFound();
            }

            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ComicExists(Guid id)
        {
            return _context.Comics.Any(e => e.Id == id);
        }
    }
}
