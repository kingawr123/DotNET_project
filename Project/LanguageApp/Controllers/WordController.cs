using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageApp.Data;
using LanguageApp.Models;

namespace LanguageApp.Controllers
{
    public class WordController : Controller
    {
        private readonly LanguageAppContext _context;

        public WordController(LanguageAppContext context)
        {
            _context = context;
        }

        // GET: Word
        public async Task<IActionResult> Index()
        {
              return _context.Word != null ? 
                          View(await _context.Word.OrderBy(w => w.Polish).ToListAsync()) :
                          Problem("Entity set 'LanguageAppContext.Word'  is null.");
        }

        // GET: Word/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Word == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.WordId == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // GET: Word/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Word/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WordId,Polish,Translation")] Word word)
        {
            if (ModelState.IsValid)
            {
                _context.Add(word);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(word);
        }

        // GET: Word/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Word == null)
            {
                return NotFound();
            }

            var word = await _context.Word.FindAsync(id);
            if (word == null)
            {
                return NotFound();
            }
            return View(word);
        }

        // POST: Word/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WordId,Polish,Translation")] Word word)
        {
            if (id != word.WordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(word);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordExists(word.WordId))
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
            return View(word);
        }

        // GET: Word/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Word == null)
            {
                return NotFound();
            }

            var word = await _context.Word
                .FirstOrDefaultAsync(m => m.WordId == id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        // POST: Word/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Word == null)
            {
                return Problem("Entity set 'LanguageAppContext.Word'  is null.");
            }
            var word = await _context.Word.FindAsync(id);
            if (word != null)
            {
                _context.Word.Remove(word);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordExists(int id)
        {
          return (_context.Word?.Any(e => e.WordId == id)).GetValueOrDefault();
        }
    }
}
