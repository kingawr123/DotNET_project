using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_webapplication.Data;
using Project_webapplication.Models;

namespace Project_webapplication.Controllers
{
    public class LoodspotController : Controller
    {
        private readonly LoodspotContext _context;

        public LoodspotController(LoodspotContext context)
        {
            _context = context;
        }

        // GET: Loodspot
        public async Task<IActionResult> Index()
        {
              return _context.Loodspot != null ? 
                          View(await _context.Loodspot.ToListAsync()) :
                          Problem("Entity set 'LoodspotContext.Loodspot'  is null.");
        }

        // GET: Loodspot/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loodspot == null)
            {
                return NotFound();
            }

            var loodspot = await _context.Loodspot
                .FirstOrDefaultAsync(m => m.IdLoodspotu == id);
            if (loodspot == null)
            {
                return NotFound();
            }

            return View(loodspot);
        }

        // GET: Loodspot/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loodspot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLoodspotu,Adres")] Loodspot loodspot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loodspot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loodspot);
        }

        // GET: Loodspot/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loodspot == null)
            {
                return NotFound();
            }

            var loodspot = await _context.Loodspot.FindAsync(id);
            if (loodspot == null)
            {
                return NotFound();
            }
            return View(loodspot);
        }

        // POST: Loodspot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLoodspotu,Adres")] Loodspot loodspot)
        {
            if (id != loodspot.IdLoodspotu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loodspot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoodspotExists(loodspot.IdLoodspotu))
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
            return View(loodspot);
        }

        // GET: Loodspot/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loodspot == null)
            {
                return NotFound();
            }

            var loodspot = await _context.Loodspot
                .FirstOrDefaultAsync(m => m.IdLoodspotu == id);
            if (loodspot == null)
            {
                return NotFound();
            }

            return View(loodspot);
        }

        // POST: Loodspot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loodspot == null)
            {
                return Problem("Entity set 'LoodspotContext.Loodspot'  is null.");
            }
            var loodspot = await _context.Loodspot.FindAsync(id);
            if (loodspot != null)
            {
                _context.Loodspot.Remove(loodspot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoodspotExists(int id)
        {
          return (_context.Loodspot?.Any(e => e.IdLoodspotu == id)).GetValueOrDefault();
        }
    }
}
