using ComicsManager.Model;
using ComicsManager.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicsManager.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Editors")]
    public class EditorsController : BaseController
    {
        public EditorsController(ComicsManagerContext context)
            : base(context)
        {

        }

        // GET: api/Editors
        [HttpGet]
        public IEnumerable<Editor> GetEditors()
        {
            return _context.Editors;
        }

        // GET: api/Editors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEditor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var editor = await _context.Editors.SingleOrDefaultAsync(m => m.Id == id);

            if (editor == null)
            {
                return NotFound();
            }

            return Ok(editor);
        }

        // PUT: api/Editors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditor([FromRoute] Guid id, [FromBody] Editor editor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editor.Id)
            {
                return BadRequest();
            }

            _context.Entry(editor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Editors
        [HttpPost]
        public async Task<IActionResult> PostEditor([FromBody] Editor editor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Editors.Add(editor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEditor", new { id = editor.Id }, editor);
        }

        // DELETE: api/Editors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEditor([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var editor = await _context.Editors.SingleOrDefaultAsync(m => m.Id == id);
            if (editor == null)
            {
                return NotFound();
            }

            _context.Editors.Remove(editor);
            await _context.SaveChangesAsync();

            return Ok(editor);
        }

        private bool EditorExists(Guid id)
        {
            return _context.Editors.Any(e => e.Id == id);
        }
    }
}