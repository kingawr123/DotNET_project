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
    public class LoginController : Controller
    {
        private readonly LanguageAppContext _context;

        public LoginController(LanguageAppContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
              return _context.LoginVM != null ? 
                          View(await _context.LoginVM.ToListAsync()) :
                          Problem("Entity set 'LanguageAppContext.LoginVM'  is null.");
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoginVM == null)
            {
                return NotFound();
            }

            var loginVM = await _context.LoginVM
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (loginVM == null)
            {
                return NotFound();
            }

            return View(loginVM);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Username,Password")] LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loginVM);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoginVM == null)
            {
                return NotFound();
            }

            var loginVM = await _context.LoginVM.FindAsync(id);
            if (loginVM == null)
            {
                return NotFound();
            }
            return View(loginVM);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password")] LoginVM loginVM)
        {
            if (id != loginVM.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginVMExists(loginVM.UserId))
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
            return View(loginVM);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoginVM == null)
            {
                return NotFound();
            }

            var loginVM = await _context.LoginVM
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (loginVM == null)
            {
                return NotFound();
            }

            return View(loginVM);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoginVM == null)
            {
                return Problem("Entity set 'LanguageAppContext.LoginVM'  is null.");
            }
            var loginVM = await _context.LoginVM.FindAsync(id);
            if (loginVM != null)
            {
                _context.LoginVM.Remove(loginVM);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginVMExists(int id)
        {
          return (_context.LoginVM?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
