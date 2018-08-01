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
    public class AuthorsController : BaseController
    {
        public AuthorsController(
            ComicsManagerContext context,
            IOptions<AppSettings> config)
            : base(context, config)
        {
            
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> photoFile, [Bind("FirstName,LastName,BirthDate,Photo,Id,CreatedOn,ModifiedOn")] Author author)
        {
            if (ModelState.IsValid)
            {
                // Upload du fichier de la photo
                if (photoFile.Count != 0)
                {
                    var file = photoFile.FirstOrDefault();
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

                    author.PhotoId = fileEntity.Id;
                }
                
                _context.Add(author);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<IFormFile> photoFile, Guid id, [Bind("FirstName,LastName,BirthDate,Photo,Id,CreatedOn,ModifiedOn")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Upload du fichier de la photo
                    if (photoFile.Count != 0)
                    {
                        var file = photoFile.FirstOrDefault();
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

                        author.PhotoId = fileEntity.Id;

                        // TODO: Suppression de la photo précédente (si existante) ?
                    }

                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
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
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Authors/DeletePhoto
        [HttpPost]
        public async Task<IActionResult> DeletePhoto(Guid? authorId, Guid? photoId)
        {
            if(authorId == Guid.Empty || photoId == Guid.Empty)
            {
                return NotFound();
            }

            var author = await _context.Authors.SingleOrDefaultAsync(m => m.Id == authorId);
            if (author == null)
            {
                return NotFound();
            }

            var file = await _context.Files.SingleOrDefaultAsync(m => m.Id == photoId);
            if(file == null)
            {
                return NotFound();
            }

            _context.Files.Remove(file);

            author.PhotoId = null;
            _context.Update(author);

            await _context.SaveChangesAsync();

            return View(nameof(Edit), author);
        }

        private bool AuthorExists(Guid id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
