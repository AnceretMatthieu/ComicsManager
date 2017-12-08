using ComicsManager.Model;
using ComicsManager.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsManager.BackOffice.Controllers
{
    public class EditorsController : BaseController
    {
        public EditorsController(ComicsManagerContext context)
            : base(context)
        {
            
        }

        // GET: Editors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Editors.ToListAsync());
        }

        // GET: Editors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // GET: Editors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Editors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedOn,ModifiedOn")] Editor editor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(editor);
        }

        // GET: Editors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors.SingleOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        // POST: Editors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id,CreatedOn,ModifiedOn")] Editor editor)
        {
            if (id != editor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorExists(editor.Id))
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
            return View(editor);
        }

        // GET: Editors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editor = await _context.Editors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            return View(editor);
        }

        // POST: Editors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var editor = await _context.Editors.SingleOrDefaultAsync(m => m.Id == id);
            _context.Editors.Remove(editor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorExists(Guid id)
        {
            return _context.Editors.Any(e => e.Id == id);
        }
    }
}
