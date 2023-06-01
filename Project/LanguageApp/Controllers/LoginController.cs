using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using System.Text;
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

        // GET: Login  <- all users
        public async Task<IActionResult> Index()
        {
            var users = _context.User.Include(u => u.Quizzes).AsNoTracking();
            return _context.User != null ? 
                View(await users.ToListAsync()) :
                Problem("Entity set 'LanguageAppContext.User'  is null.");
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User.Include(u => u.Quizzes)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
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
        public async Task<IActionResult> Create([Bind("UserId,Username,Password")] User User)
        {
            
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            // Console.WriteLine(errors.ToList()[0].ErrorMessage);
            
            if (ModelState.IsValid)
            {

                _context.Add(User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(User);
        }


        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            return View(User);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password")] User User)
        {
            if (id != User.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(User);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(User.UserId))
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
            return View(User);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var User = await _context.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (User == null)
            {
                return NotFound();
            }

            return View(User);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'LanguageAppContext.User'  is null.");
            }
            var User = await _context.User.FindAsync(id);
            if (User != null)
            {
                _context.User.Remove(User);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        public bool DoesUserExists(string login, string pass)
        {
          return (_context.User?.Any(e => e.Username == login && e.Password == pass)).GetValueOrDefault();
        }


        static String skrotMD5(String napis)
        {
            Encoding enc = Encoding.UTF8;
            var hashBuilder = new StringBuilder();
            using var hash = MD5.Create();
            byte[] result = hash.ComputeHash(enc.GetBytes(napis));
            foreach (var b in result)
                hashBuilder.Append(b.ToString("x2"));
            return hashBuilder.ToString();
        }
    }
}


