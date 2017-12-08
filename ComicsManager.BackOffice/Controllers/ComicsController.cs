using ComicsManager.Model;
using ComicsManager.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsManager.BackOffice.Controllers
{
    public class ComicsController : BaseController
    {
        public ComicsController(ComicsManagerContext context)
            : base(context)
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
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
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
        public async Task<IActionResult> Create([Bind("Title,ISBN,Cycle,Collection,Note,Couverture,PublicationDate,Id,CreatedOn,ModifiedOn")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comic);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,ISBN,Cycle,Collection,Note,Couverture,PublicationDate,Id,CreatedOn,ModifiedOn")] Comic comic)
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

        // GET: Comics/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comic = await _context.Comics
                .SingleOrDefaultAsync(m => m.Id == id);
            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comic = await _context.Comics.SingleOrDefaultAsync(m => m.Id == id);
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
