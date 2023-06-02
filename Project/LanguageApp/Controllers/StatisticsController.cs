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
    public class StatisticsController : Controller
    {
        private readonly LanguageAppContext _context;

        public StatisticsController(LanguageAppContext context)
        {
            _context = context;
        }

        // GET: Statistics
        public async Task<IActionResult> Index()
        {
              return _context.Statistics != null ? 
                          View(await _context.Statistics.ToListAsync()) :
                          Problem("Entity set 'LanguageAppContext.Statistics'  is null.");
        }

        // GET: Statistics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.StatisticsId == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // GET: Statistics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statistics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatisticsId,QuizCounter,AverageScore")] Statistics statistics)
        {
            string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn == "false")
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                _context.Add(statistics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statistics);
        }

        // GET: Statistics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn == "false")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics == null)
            {
                return NotFound();
            }
            return View(statistics);
        }

        // POST: Statistics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatisticsId,QuizCounter,AverageScore")] Statistics statistics)
        {
            string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn == "false")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != statistics.StatisticsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatisticsExists(statistics.StatisticsId))
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
            return View(statistics);
        }

        // GET: Statistics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn == "false")
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Statistics == null)
            {
                return NotFound();
            }

            var statistics = await _context.Statistics
                .FirstOrDefaultAsync(m => m.StatisticsId == id);
            if (statistics == null)
            {
                return NotFound();
            }

            return View(statistics);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string isLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (isLoggedIn == "false")
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Statistics == null)
            {
                return Problem("Entity set 'LanguageAppContext.Statistics'  is null.");
            }
            var statistics = await _context.Statistics.FindAsync(id);
            if (statistics != null)
            {
                _context.Statistics.Remove(statistics);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatisticsExists(int id)
        {
          return (_context.Statistics?.Any(e => e.StatisticsId == id)).GetValueOrDefault();
        }
    }
}
